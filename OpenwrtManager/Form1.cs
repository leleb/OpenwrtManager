using Renci.SshNet;
using Renci.SshNet.Common;
using System;
using System.Data;
using System.Net;
using System.Net.NetworkInformation;
using System.Security.Policy;
using System.Text.RegularExpressions;

namespace OpenwrtManager
{
  public partial class Form1 : Form
  {
    public Form1()
    {
      InitializeComponent();
      textBoxIP.Text = GetDefaultGateway().ToString();
    }

    #region SSH Connection
    private IPAddress GetDefaultGateway()
    {
#pragma warning disable CS8600 // Conversione del valore letterale Null o di un possibile valore Null in un tipo che non ammette i valori Null.
      IPAddress GWAddr = NetworkInterface
      .GetAllNetworkInterfaces()
      .Where(n => n.OperationalStatus == OperationalStatus.Up)
      .Where(n => n.NetworkInterfaceType != NetworkInterfaceType.Loopback)
      .SelectMany(n => n.GetIPProperties()?.GatewayAddresses)
      .Select(g => g?.Address)
      .FirstOrDefault(a => a != null);
#pragma warning restore CS8600 // Conversione del valore letterale Null o di un possibile valore Null in un tipo che non ammette i valori Null.
      if (null == GWAddr) GWAddr = IPAddress.None;
      return GWAddr;
    }

    string User = "root";
    SshClient client;
    ShellStream shellStream;
    string reply = string.Empty;

    private void ShellStream_DataReceived(object? sender, ShellDataEventArgs e)
    {
      //reply = shellStream.Expect(new Regex(@":.[*>#]"), new TimeSpan(0, 0, 3));
      reply = shellStream.Read();
      Invoke((MethodInvoker)(() => ParseAns(reply)));
    }

    private void buttonConnect_Click(object sender, EventArgs e)
    {
      try
      {
        if (client == null)
        {
          UInt16 Port;
          UInt16.TryParse(textBoxPort.Text, out Port);
          client = new SshClient(textBoxIP.Text, Port, User, textBoxPwd.Text);
          client.ConnectionInfo.Timeout = TimeSpan.FromSeconds(5);
          client.Connect();
          shellStream = client.CreateShellStream("ansi", 80, 60, 800, 600, 65536);
          // trigger DataReceived event asap prompt received or after 3 sec timeout
          reply = shellStream.Expect(new Regex(@":.[*>#]"), new TimeSpan(0, 0, 3));
          richTextBoxAns.AppendText(reply); // get welcome message
          shellStream.DataReceived += ShellStream_DataReceived; // bind answer event

          richTextBoxAns.SelectionStart = richTextBoxAns.Text.Length; richTextBoxAns.ScrollToCaret();
          buttonConnect.Text = "Disconnect";
          groupBoxTerminal.Enabled = true;

          Application.DoEvents(); Thread.Sleep(200);
        }
        else
        {
          client.Disconnect(); client.Dispose(); client = null;
          buttonConnect.Text = "Connect"; groupBoxTerminal.Enabled = false;
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.ToString());
      }
    }
    #endregion

    #region Terminal tab
    private void buttonSend_Click(object sender, EventArgs e)
    {
      shellStream.Write(textBoxCommand.Text + "\n");
      shellStream.Flush();
    }

    private void richTextBoxAns_DoubleClick(object sender, EventArgs e)
    {
      richTextBoxAns.Text = string.Empty;
    }
    #endregion

    #region Port forward tab
    DataTable dt;
    DataRow row;
    int OldRuleIndex = -1;
    bool EnableParseFirewallRedirect = false;

    private void ParseAns(string Ans)
    {
      richTextBoxAns.AppendText(Ans);
      if (EnableParseFirewallRedirect)
      {
        using (StringReader reader = new StringReader(Ans))
        {
          string line;

          while ((line = reader.ReadLine()) != null)
          {
            line = line.Trim();
            if (line.EndsWith("#"))
            {
              if (OldRuleIndex >= 0) dt.Rows.Add(row); // add last row if any
              OldRuleIndex = -1; EnableParseFirewallRedirect = false; dataGridViewPortForward.DataSource = dt;
            }
            if (line.StartsWith("firewall.@redirect["))
            { // a firewall port forward rule
              int s, e;

              s = line.IndexOf("[") + 1; e = line.IndexOf("]");
              int RuleIndex = int.Parse(line.Substring(s, e - s)); // get rule index
              if (OldRuleIndex != RuleIndex)
              { // a new rule
                if (OldRuleIndex >= 0) dt.Rows.Add(row); // not first rule add previous row
                row = dt.NewRow(); OldRuleIndex = RuleIndex;
              }
              s = line.IndexOf("].") + 2; e = line.IndexOf("=");  // get field name
              if (s > 2)
              {
                string FieldName = line.Substring(s, e - s);
                s = line.IndexOf("'") + 1; e = line.LastIndexOf("'");
                string FieldValue = line.Substring(s - 1, e - s + 2); // get field value
                /*
                Invoke((MethodInvoker)(() => richTextBoxAns.AppendText(
                  "Rule#" + RuleIndex.ToString() + ": " + FieldName + " = " + FieldValue + Environment.NewLine
                  )));
                */
                if (FieldName == "name") row[0] = FieldValue;
                else if (FieldName == "dest_port") row[1] = FieldValue;
                else if (FieldName == "dest_ip") row[2] = FieldValue;
                else if (FieldName == "proto")
                {
                  List<string> stringList = FieldValue.Split(' ').ToList();
                  FieldValue = "";
                  foreach (string str in stringList)
                  {
                    if (!str.StartsWith("'")) FieldValue = FieldValue + "'";
                    FieldValue = FieldValue + str;
                    if (!FieldValue.EndsWith("'")) FieldValue = FieldValue + "'";
                    FieldValue = FieldValue + " ";
                  }
                  FieldValue = FieldValue.Trim();
                  row[3] = FieldValue;
                }
                else if (FieldName == "src_dport") row[4] = FieldValue;
                else if (FieldName == "src_dip") row[5] = FieldValue;
                else if (FieldName == "src_ip") row[6] = FieldValue;
                else if (FieldName == "src") row[7] = FieldValue;
                else if (FieldName == "dest") row[8] = FieldValue;
                else if (FieldName == "enabled") row[9] = FieldValue;
              }
            }
          }
        }
      }
    }

    char Separator = ';';
    private void buttonOpenFile_Click(object sender, EventArgs e)
    {
      using (OpenFileDialog ofd = new OpenFileDialog() { Filter = "CSV file|*.csv" })
      {
        if (ofd.ShowDialog() != DialogResult.OK) return;
        DataTable dt = new DataTable();
        using (StreamReader streamReader = new StreamReader(ofd.FileName))
        {
          while (!streamReader.EndOfStream)
          {
            string text = streamReader.ReadToEnd();
            string[] rows = text.Split('\n');
            if (rows.Length > 0)
            {
              string[] columns = rows[0].Split(Separator); //Add columns
              for (int j = 0; j < columns.Count(); j++)
                dt.Columns.Add(columns[j]);
              for (int i = 1; i < rows.Count(); i++)
              { //Add rows
                string[] data = rows[i].Split(Separator);
                DataRow dr = dt.NewRow();
                for (int k = 0; k < data.Count(); k++) dr[k] = data[k];
                dt.Rows.Add(dr);
              }
            }
          }
        }
        dataGridViewPortForward.DataSource = dt;
        //remove last row
        dataGridViewPortForward.Rows.Remove(dataGridViewPortForward.Rows[dataGridViewPortForward.Rows.Count - 2]);
      }
    }
    private void buttonSaveFile_Click(object sender, EventArgs e)
    {
      if (dataGridViewPortForward.Rows.Count > 0)
      {
        SaveFileDialog sfd = new SaveFileDialog();
        sfd.Filter = "CSV (*.csv)|*.csv";
        sfd.FileName = ".csv";
        if (sfd.ShowDialog() != DialogResult.OK) return;

        try
        {
          StreamWriter csvFileWriter = new StreamWriter(sfd.FileName, false);

          IEnumerable<DataGridViewCell> cells = dataGridViewPortForward.Rows[0].Cells.Cast<DataGridViewCell>();
          string Line = string.Join(Separator, cells.Select(cell => cell.OwningColumn.HeaderText).ToArray());
          Line = Line.Trim();
          csvFileWriter.WriteLine(Line);
          for (int i = 0; i < dataGridViewPortForward.RowCount - 1; i++)
          {
            cells = dataGridViewPortForward.Rows[i].Cells.Cast<DataGridViewCell>();
            Line = string.Join(Separator, cells.Select(cell => cell.Value).ToArray());
            Line = Line.Trim();
            if (Line.Length > cells.Count() - 1)
              csvFileWriter.WriteLine(Line);
          }
          csvFileWriter.Close();
        }
        catch (Exception ex)
        {
          MessageBox.Show("Error :" + ex.Message);
        }
      }
    }
    private void buttonGetRedirectRouter_Click(object sender, EventArgs e)
    {
      dt = new DataTable();
      OldRuleIndex = -1;
      EnableParseFirewallRedirect = true;
      // add columns header
      dt.Columns.Add("Name (name)");
      dt.Columns.Add("Internal port (dest_port)");
      dt.Columns.Add("Internal IP address (dest_ip)");
      dt.Columns.Add("Protocol (proto)");
      dt.Columns.Add("External port (src_dport)");
      dt.Columns.Add("External IP address (src_dip)");
      dt.Columns.Add("Source IP address (src_ip)");
      dt.Columns.Add("Source zone (src)");
      dt.Columns.Add("Destination zone (dest)");
      dt.Columns.Add("Enabled");
      //dt.Columns.Add("DNAT (target)");
      shellStream.Write("uci show firewall\n");
    }
    private void buttonSetRedirectRouter_Click(object sender, EventArgs e)
    {
      shellStream.Write("while uci -q delete firewall.@redirect[-1]; do :; done\n"); // delete all rules
      //shellStream.Write("#!/bin/sh\n");
      //shellStream.Write("uci batch << EOI\n");
      for (int i = 0; i < dataGridViewPortForward.RowCount - 1; i++)
      {
        shellStream.Write("uci add firewall redirect\n");
        string CellValue = dataGridViewPortForward.Rows[i].Cells[0].Value.ToString().Trim();
        if (CellValue.Length > 0)
          // for ex. // set firewall.@redirect[1].name='TestUciRuleM
          //shellStream.Write("set firewall.@redirect[-1].name='" + CelllValue + "'\n");
          shellStream.Write("uci set firewall.@redirect[-1].name=" + CellValue + "\n");
        CellValue = dataGridViewPortForward.Rows[i].Cells[1].Value.ToString().Trim();
        if (CellValue.Length > 0)
          shellStream.Write("uci set firewall.@redirect[-1].dest_port=" + CellValue + "\n");
        CellValue = dataGridViewPortForward.Rows[i].Cells[2].Value.ToString().Trim();
        if (CellValue.Length > 0)
          shellStream.Write("uci set firewall.@redirect[-1].dest_ip=" + CellValue + "\n");
        CellValue = dataGridViewPortForward.Rows[i].Cells[3].Value.ToString().Trim();
        if (CellValue.Length > 0)
        { // split CellValue and send multiple add_list
          List<string> stringList = CellValue.Split(' ').ToList();
          foreach (string str in stringList)
          { shellStream.Write("uci add_list firewall.@redirect[-1].proto=" + str + "\n"); }
        }
        CellValue = dataGridViewPortForward.Rows[i].Cells[4].Value.ToString().Trim();
        if (CellValue.Length > 0)
          shellStream.Write("uci set firewall.@redirect[-1].src_dport=" + CellValue + "\n");
        CellValue = dataGridViewPortForward.Rows[i].Cells[5].Value.ToString();
        if (CellValue.Length > 0)
          shellStream.Write("uci set firewall.@redirect[-1].src_dip=" + CellValue + "\n");
        CellValue = dataGridViewPortForward.Rows[i].Cells[6].Value.ToString().Trim();
        if (CellValue.Length > 0)
          shellStream.Write("uci set firewall.@redirect[-1].src_ip=" + CellValue + "\n");
        CellValue = dataGridViewPortForward.Rows[i].Cells[7].Value.ToString().Trim();
        if (CellValue.Length > 0)
          shellStream.Write("uci set firewall.@redirect[-1].src=" + CellValue + "\n");
        CellValue = dataGridViewPortForward.Rows[i].Cells[8].Value.ToString().Trim();
        if (CellValue.Length > 0)
          shellStream.Write("uci set firewall.@redirect[-1].dest=" + CellValue + "\n");
        CellValue = dataGridViewPortForward.Rows[i].Cells[9].Value.ToString().Trim();
        if (CellValue.Length > 0)
          shellStream.Write("uci set firewall.@redirect[-1].enabled=" + CellValue + "\n");
        //shellStream.Write("uci set firewall.@redirect[-1].target='DNAT'\n");
      }
      //shellStream.Write("commit firewall\n"); shellStream.Write("EOI\n");
      shellStream.Write("uci commit firewall\n");
      shellStream.Write("service firewall restart\n");
    }
    #endregion
  }
}
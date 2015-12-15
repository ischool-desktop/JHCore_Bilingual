using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;

namespace JHSchool.Permrec.StudentExtendControls.Ribbon
{
    public partial class BatchAddressLatitudeForm : FISCA.Presentation.Controls.BaseForm 
    {
        private BackgroundWorker _BGWork;
        private BatchAddressLatitudeManager.AddressTye _addRecType;
        private List<string> _StudIDList;

        public BatchAddressLatitudeForm()
        {
            InitializeComponent();
        }

        private void BatchAddressLatitudeForm_Load(object sender, EventArgs e)
        {
            cboAddressType.Items.Add("戶籍地址");
            cboAddressType.Items.Add("聯絡地址");
            cboAddressType.Items.Add("其它地址1");
            cboAddressType.SelectedIndex = 0;
            cboAddressType.DropDownStyle = ComboBoxStyle.DropDownList;
            _addRecType = BatchAddressLatitudeManager.AddressTye.Permanent;
            _StudIDList = new List<string>();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            _StudIDList = K12.Presentation.NLDPanels.Student.SelectedSource;

            if (_StudIDList.Count < 1)
            {
                FISCA.Presentation.Controls.MsgBox.Show("請選擇學生.");
                return;
            }

            FISCA.Presentation.MotherForm.SetStatusBarMessage("資料產生中..");
            btnRun.Enabled = false;            

            if (cboAddressType.Text == "戶籍地址")
                _addRecType = BatchAddressLatitudeManager.AddressTye.Permanent;

            if (cboAddressType.Text == "聯絡地址")
                _addRecType = BatchAddressLatitudeManager.AddressTye.Mailing;

            if (cboAddressType.Text == "其它地址1")
                _addRecType = BatchAddressLatitudeManager.AddressTye.Address1;
            

            _BGWork = new BackgroundWorker();
            _BGWork.DoWork += new DoWorkEventHandler(_BGWork_DoWork);
            _BGWork.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_BGWork_RunWorkerCompleted);
            _BGWork.RunWorkerAsync();
        }

        void _BGWork_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Student.Instance.SyncAllBackground();
            btnRun.Enabled = true;
            FISCA.Presentation.MotherForm.SetStatusBarMessage("");
            FISCA.Presentation.Controls.MsgBox.Show("產生完成.");
            PermRecLogProcess prlp = new PermRecLogProcess();
            prlp.SaveLog("學籍.學生", "查詢學生地址經緯度", "共查詢" + _StudIDList.Count + "筆學生" + cboAddressType.Text + "經緯度");
            
            this.Close();
        }

        void _BGWork_DoWork(object sender, DoWorkEventArgs e)
        {
            BatchAddressLatitudeManager balm = new BatchAddressLatitudeManager(_StudIDList, _addRecType);
        }
    }
}

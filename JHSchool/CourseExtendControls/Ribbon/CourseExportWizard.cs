using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
//using SmartSchool.SmartPlugIn.Student.Export.RequestHandler.Formater;
//using SmartSchool.SmartPlugIn.Student.Export.RequestHandler;
//using SmartSchool.SmartPlugIn.Student.Export.ResponseHandler;
//using SmartSchool.SmartPlugIn.Student.Export.ResponseHandler.Output;
//using SmartSchool.SmartPlugIn.Student.Export.ResponseHandler.Connector;
using DevComponents.DotNetBar;
using System.Diagnostics;
using FISCA.Presentation.Controls;
using JHSchool.Legacy.Export.RequestHandler;
using JHSchool.Legacy.Export.ResponseHandler;
using JHSchool.Legacy.Export.ResponseHandler.Connector;
using JHSchool.Legacy.Export.RequestHandler.Formater;
using Framework;
using JHSchool.Legacy.Export.ResponseHandler.Output;

namespace JHSchool.CourseExtendControls.Ribbon
{
    public partial class CourseExportWizard : BaseForm
    {
        public CourseExportWizard()
        {
            InitializeComponent();
        }

        private void ExportForm_Load(object sender, EventArgs e)
        {
            XmlElement element = SmartSchool.Feature.Course.CourseBulkProcess.GetExportDescription();
            BaseFieldFormater formater = new BaseFieldFormater();
            FieldCollection collection = formater.Format(element);

            List<string> list = new List<string>(new string[] { "�ҵ{�t�νs��", "�ҵ{�W��", "�Ǧ~��", "�Ǵ�" });

            foreach (Field field in collection)
            {
                ListViewItem item = listView.Items.Add(field.DisplayText);
                if (list.Contains(field.DisplayText))
                {
                    item.ForeColor = Color.Red;
                }
                item.Tag = field;
                item.Checked = true;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (GetSelectedFields().Count == 0)
            {
                FISCA.Presentation.Controls.MsgBox.Show("�����ܤֿ�ܤ@���ץX���!", "���ť�", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            saveFileDialog1.Filter = "Excel (*.xls)|*.xls|�Ҧ��ɮ� (*.*)|*.*";
            saveFileDialog1.FileName = "�ץX�ҵ{�򥻸��";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                IExportConnector ec = new ExportCourseConnector();
                foreach (CourseRecord course in Course.Instance.SelectedList)
                {
                    ec.AddCondition(course.ID);
                }
                ec.SetSelectedFields(GetSelectedFields());
                ExportTable table = ec.Export();

                ExportOutput output = new ExportOutput();
                output.SetSource(table);
                output.Save(saveFileDialog1.FileName);

                if (FISCA.Presentation.Controls.MsgBox.Show("�ɮצs�ɧ����A�O�_�}�Ҹ��ɮ�", "�O�_�}��", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        Process.Start(saveFileDialog1.FileName);
                    }
                    catch (Exception ex)
                    {
                        FISCA.Presentation.Controls.MsgBox.Show("�}���ɮ׵o�ͥ���:" + ex.Message, "���~", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                this.Close();
            }
        }

        private FieldCollection GetSelectedFields()
        {
            FieldCollection collection = new FieldCollection();
            foreach (ListViewItem item in listView.CheckedItems)
            {
                Field field = item.Tag as Field;
                collection.Add(field);
            }
            return collection;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void chkSelect_CheckedChanged(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listView.Items)
            {
                item.Checked = chkSelect.Checked;
            }
        }
    }
}
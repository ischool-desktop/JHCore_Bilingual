using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Aspose.Cells;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace JHSchool.Permrec.ClassExtendControls.Reports
{
    class ClassCellReportManger
    {
        private string _SaveFileName;
        private string _WorksheetName;
        private Workbook _wb;
        private Worksheet _wst;
        private BackgroundWorker _bkWork;
        private enum RptProcessType { 男女比例, 里鄰統計, 職業統計, 年齡統計, 類別統計 };

        public ClassCellReportManger()
        {

        }

        /// <summary>
        /// 處理男女比例
        /// </summary>
        /// <param name="SaveFileName"></param>
        /// <param name="WorksheetName"></param>
        public void ProcessClassGenderPercentage(string SaveFileName, string WorksheetName, Dictionary<string, List<DAL.ClassStudentCount>> ClassStudentCountDic, Dictionary<string, DAL.ClassStudentCount> GradeStudentCountDic, List<DAL.StudentEntity> ErrorData)
        {
            _SaveFileName = SaveFileName;
            _WorksheetName = WorksheetName;
            RptProcessType rpt = RptProcessType.男女比例;

            object[] data = new object[4] { rpt, ClassStudentCountDic, GradeStudentCountDic, ErrorData };
            _bkWork = new BackgroundWorker();
            _bkWork.DoWork += new DoWorkEventHandler(_bkWork_DoWork);
            _bkWork.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_bkWork_RunWorkerCompleted);
            _bkWork.RunWorkerAsync(data);
        }

        /// <summary>
        /// 處理學生父母職業統計
        /// </summary>
        /// <param name="?"></param>
        /// <param name="?"></param>
        /// <param name="WorksheetName"></param>
        /// <param name="?"></param>
        public void ProcessClassStudParentJobCount(string SaveFileName, string WorksheetName, DAL.StudentParentJobCounter StudParentJobCounter, DAL.StudentParentJobCounter.ParentJobType ParentJobType, List<string> selectJobItems)
        {
            _SaveFileName = SaveFileName;
            _WorksheetName = WorksheetName;
            RptProcessType rpt = RptProcessType.職業統計;
            object[] data = new object[4] { rpt, StudParentJobCounter, ParentJobType, selectJobItems };
            _bkWork = new BackgroundWorker();
            _bkWork.DoWork += new DoWorkEventHandler(_bkWork_DoWork);
            _bkWork.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_bkWork_RunWorkerCompleted);
            _bkWork.RunWorkerAsync(data);
        }

        /// <summary>
        /// 處理里鄰統計
        /// </summary>
        /// <param name="SaveFileName"></param>
        /// <param name="WorksheetName"></param>
        /// <param name="?"></param>
        public void ProcessClassDistrictAreaCount(string SaveFileName, string WorksheetName, DAL.DistrictAreaCounter dac, List<DAL.StudentEntity> ErrorData)
        {
            _SaveFileName = SaveFileName;
            _WorksheetName = WorksheetName;
            RptProcessType rpt = RptProcessType.里鄰統計;

            object[] data = new object[3] { rpt, dac, ErrorData };
            _bkWork = new BackgroundWorker();
            _bkWork.DoWork += new DoWorkEventHandler(_bkWork_DoWork);
            _bkWork.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_bkWork_RunWorkerCompleted);
            _bkWork.RunWorkerAsync(data);

        }

        /// <summary>
        /// 處理學生類別統計
        /// </summary>
        /// <param name="SaveFileName"></param>
        /// <param name="WorksheetName"></param>
        /// <param name="stc"></param>
        public void ProcessClassStudentTagCount(string SaveFileName, string WorksheetName, DAL.StudentTagCounter stc)
        {
            _SaveFileName = SaveFileName;
            _WorksheetName = WorksheetName;
            RptProcessType rpt = RptProcessType.類別統計;
            object[] data = new object[2] { rpt, stc };
            _bkWork = new BackgroundWorker();
            _bkWork.DoWork += new DoWorkEventHandler(_bkWork_DoWork);
            _bkWork.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_bkWork_RunWorkerCompleted);
            _bkWork.RunWorkerAsync(data);
        }

        /// <summary>
        /// 處理班級學生年齡統計
        /// </summary>
        /// <param name="SaveFileName"></param>
        /// <param name="WorksheetName"></param>
        /// <param name="?"></param>
        public void ProcessClassStudentAgeCount(string SaveFileName, string WorksheetName, DAL.StudentAgeCounter sac, List<DAL.StudentEntity> ErrorData)
        {
            _SaveFileName = SaveFileName;
            _WorksheetName = WorksheetName;
            RptProcessType rpt = RptProcessType.年齡統計;
            object[] data = new object[3] { rpt, sac, ErrorData };
            _bkWork = new BackgroundWorker();
            _bkWork.DoWork += new DoWorkEventHandler(_bkWork_DoWork);
            _bkWork.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_bkWork_RunWorkerCompleted);
            _bkWork.RunWorkerAsync(data);
        }


        void _bkWork_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Workbook _wb = (Workbook)e.Result;

             // 處理存檔
            //try
            //{
            //    _SaveFileName += ".xls";
            //    _wb.Worksheets[0].Name = _WorksheetName;
            //    _wb.Save(Application.StartupPath + "\\Reports\\" + _SaveFileName, FileFormatType.Excel2003);
            //    System.Diagnostics.Process.Start(Application.StartupPath + "\\Reports\\" + _SaveFileName);
            //}
            //catch
            //{
                System.Windows.Forms.SaveFileDialog sd1 = new SaveFileDialog();
                sd1.Title = "另存新檔";
                sd1.FileName = _SaveFileName;
                sd1.Filter = "Excel檔案 (*.xls)|*.xls|所有檔案 (*.*)|*.*";
                
                if (sd1.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        _wb.Save(sd1.FileName, FileFormatType.Excel2003);
                        System.Diagnostics.Process.Start(sd1.FileName);
                    }
                    catch
                    {
                        System.Windows.Forms.MessageBox.Show("指定路徑無法存取。", "建立檔案失敗", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        return;
                    }
                }
            //}

        }

        void _bkWork_DoWork(object sender, DoWorkEventArgs e)
        {
            object[] arg = (object[])e.Argument;

            RptProcessType rpt = (RptProcessType)arg[0];

            #region 男女比例
            if (rpt == RptProcessType.男女比例)
            {
                Dictionary<string, List<DAL.ClassStudentCount>> ClassStudentCountDic = (Dictionary<string, List<DAL.ClassStudentCount>>)arg[1];
                Dictionary<string, DAL.ClassStudentCount> GradeStudentCountDic = (Dictionary<string, DAL.ClassStudentCount>)arg[2];
                List<DAL.StudentEntity> ErrorData = (List<DAL.StudentEntity>)arg[3];

                _wb = new Workbook();
                _wst = _wb.Worksheets[0];

                List<string> wsTitle = new List<string>();
                wsTitle.Add("年級班級");
                wsTitle.Add("男生人數");
                wsTitle.Add("男生比例(％)");
                wsTitle.Add("女生人數");
                wsTitle.Add("女生比例(％)");
                wsTitle.Add("未分性別人數");
                wsTitle.Add("總人數");
                wsTitle.Add("班導師");

                int Row = 1, Col = 0;
                for (Col = 0; Col < wsTitle.Count; Col++)
                {
                    //    _wst.Cells[0, Col].Style.Font.Size = 12;
                    //    _wst.Cells[0, Col].Style.Pattern = BackgroundType.Solid;
                    //    _wst.Cells[0, Col].Style.ForegroundColor = Color.Yellow;
                    _wst.Cells[0, Col].PutValue(wsTitle[Col]);
                }

                foreach (KeyValuePair<string, List<DAL.ClassStudentCount>> ClassStudPer in ClassStudentCountDic)
                {
                    foreach (DAL.ClassStudentCount csc in ClassStudPer.Value)
                    {
                        for (Col = 0; Col <= 5; Col++)
                            _wst.Cells[Row, Col].Style.Font.Size = 12;

                        _wst.Cells[Row, 0].PutValue(csc.ClassName);
                        _wst.Cells[Row, 1].PutValue(csc.GetBoyCount());
                        _wst.Cells[Row, 2].PutValue(csc.GetBoyPercentage(2));
                        _wst.Cells[Row, 3].PutValue(csc.GetGirlCount());
                        _wst.Cells[Row, 4].PutValue(csc.GetGirlPercentage(2));
                        _wst.Cells[Row, 5].PutValue(csc.GetNoGenderCount());
                        _wst.Cells[Row, 6].PutValue(csc.GetTotalCount());
                        _wst.Cells[Row, 7].PutValue(csc.TeacherName);
                        Row++;
                    }
                    if (GradeStudentCountDic.ContainsKey(ClassStudPer.Key))
                    {
                        DAL.ClassStudentCount csc = GradeStudentCountDic[ClassStudPer.Key];

                        //for (Col = 0; Col <= 5; Col++)
                        //{
                        //    _wst.Cells[Row, Col].Style.Font.Size = 12;
                        //    _wst.Cells[Row, Col].Style.Pattern = BackgroundType.Solid;
                        //    _wst.Cells[Row, Col].Style.ForegroundColor = Color.Yellow;
                        //}

                        _wst.Cells[Row, 0].PutValue(csc.ClassName + "年級");
                        _wst.Cells[Row, 1].PutValue(csc.GetBoyCount());
                        _wst.Cells[Row, 2].PutValue(csc.GetBoyPercentage(2));
                        _wst.Cells[Row, 3].PutValue(csc.GetGirlCount());
                        _wst.Cells[Row, 4].PutValue(csc.GetGirlPercentage(2));
                        _wst.Cells[Row, 5].PutValue(csc.GetNoGenderCount());
                        _wst.Cells[Row, 6].PutValue(csc.GetTotalCount());
                        Row++;
                    }
                }


                // 當有錯誤訊息
                if (ErrorData.Count > 0)
                {
                    int wstIdx = _wb.Worksheets.Add();
                    Worksheet wstErr = _wb.Worksheets[wstIdx];
                    wstErr.Name = "未分性別清單";
                    int ro = 1;
                    wstErr.Cells[0, 0].PutValue("學號");
                    wstErr.Cells[0, 1].PutValue("班級");
                    wstErr.Cells[0, 2].PutValue("座號");
                    wstErr.Cells[0, 3].PutValue("姓名");
                    wstErr.Cells[0, 4].PutValue("備註");

                    foreach (DAL.StudentEntity se in ErrorData)
                    {
                        wstErr.Cells[ro, 0].PutValue(se.StudentNumber);
                        wstErr.Cells[ro, 1].PutValue(se.ClassName);
                        wstErr.Cells[ro, 2].PutValue(se.SeatNo);
                        wstErr.Cells[ro, 3].PutValue(se.Name);
                        wstErr.Cells[ro, 4].PutValue(se.Memo);
                        ro++;
                    }
                }


                // 畫表

                Style st2 = _wb.Styles[_wb.Styles.Add()];
                StyleFlag sf2 = new StyleFlag();
                sf2.Borders = true;

                st2.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
                st2.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;
                st2.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
                st2.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
                int tmpMaxRow = 0, tmpMaxCol = 0;
                tmpMaxRow = _wst.Cells.MaxDataRow + 1;
                tmpMaxCol = _wst.Cells.MaxDataColumn + 1;
                _wst.Cells.CreateRange(0, 0, tmpMaxRow, tmpMaxCol).ApplyStyle(st2, sf2);

                for (int wstIdx = 0; wstIdx < _wb.Worksheets.Count; wstIdx++)
                {

                    tmpMaxRow = _wb.Worksheets[wstIdx].Cells.MaxDataRow + 1;
                    tmpMaxCol = _wb.Worksheets[wstIdx].Cells.MaxDataColumn + 1;
                    _wb.Worksheets[wstIdx].Cells.CreateRange(0, 0, tmpMaxRow, tmpMaxCol).ApplyStyle(st2, sf2);
                    for (int r = 0; r <= _wb.Worksheets[wstIdx].Cells.MaxDataRow; r++)
                        for (int c = 0; c <= _wb.Worksheets[wstIdx].Cells.MaxDataColumn; c++)
                            _wb.Worksheets[wstIdx].Cells[r, c].Style.Font.Size = 12;

                    _wb.Worksheets[wstIdx].AutoFitColumns();
                    _wb.Worksheets[wstIdx].AutoFitRows();
                }


            }
            #endregion


            #region 里鄰統計
            if (rpt == RptProcessType.里鄰統計)
            {
                DAL.DistrictAreaCounter dacTer = (DAL.DistrictAreaCounter)arg[1];
                List<DAL.StudentEntity> ErrorData = (List<DAL.StudentEntity>)arg[2];
                _wb = new Workbook();
                _wst = _wb.Worksheets[0];
                List<string> wsTitle = new List<string>();
                wsTitle.Add("里");
                wsTitle.Add("鄰");
                wsTitle.Add("男生人數");
                wsTitle.Add("女生人數");
                wsTitle.Add("未分性別人數");
                wsTitle.Add("總人數");
                int row = 0, col = 0;
                foreach (string str in wsTitle)
                    _wst.Cells[row, col++].PutValue(str);

                row = 1;
                int DistrictBoyCount = 0, DistrictGirlCount = 0, DistrictNoGenderCount = 0, DistrictTotalCount = 0;
                int SumDistrictBoyCount = 0, SumDistrictGirlCount = 0, SumDistrictNoGenderCount = 0, SumDistrictTotalCount = 0;
                foreach (KeyValuePair<string, List<string>> data in dacTer.GetDistrictAreName())
                {
                    // 初始值
                    DistrictBoyCount = DistrictGirlCount = DistrictNoGenderCount = DistrictTotalCount = 0;

                    _wst.Cells.CreateRange(row, 0, data.Value.Count, 1).Merge();
                    _wst.Cells[row, 0].PutValue(data.Key);
                    foreach (string val in data.Value)
                    {
                        _wst.Cells[row, 1].PutValue(val);
                        int BoyCount = dacTer.GetDistricAreaCount(data.Key, val).GetBoyCount();
                        _wst.Cells[row, 2].PutValue(BoyCount);
                        int GirlCount = dacTer.GetDistricAreaCount(data.Key, val).GetGirlCount();
                        _wst.Cells[row, 3].PutValue(GirlCount);
                        int NoGenderCount = dacTer.GetDistricAreaCount(data.Key, val).GetNoGenderCount();
                        _wst.Cells[row, 4].PutValue(NoGenderCount);
                        int TotalCount = dacTer.GetDistricAreaCount(data.Key, val).GetTotalCount();
                        _wst.Cells[row, 5].PutValue(TotalCount);

                        DistrictBoyCount += BoyCount;
                        DistrictGirlCount += GirlCount;
                        DistrictNoGenderCount += NoGenderCount;
                        DistrictTotalCount += TotalCount;

                        row++;
                    }

                    // 小計
                    _wst.Cells[row, 0].PutValue("小計");
                    _wst.Cells[row, 2].PutValue(DistrictBoyCount);
                    _wst.Cells[row, 3].PutValue(DistrictGirlCount);
                    _wst.Cells[row, 4].PutValue(DistrictNoGenderCount);
                    _wst.Cells[row, 5].PutValue(DistrictTotalCount);


                    SumDistrictBoyCount += DistrictBoyCount;
                    SumDistrictGirlCount += DistrictGirlCount;
                    SumDistrictNoGenderCount += DistrictNoGenderCount;
                    SumDistrictTotalCount += DistrictTotalCount;
                    row++;

                }
                // 總計
                _wst.Cells[row, 0].PutValue("總計");
                _wst.Cells[row, 2].PutValue(SumDistrictBoyCount);
                _wst.Cells[row, 3].PutValue(SumDistrictGirlCount);
                _wst.Cells[row, 4].PutValue(SumDistrictNoGenderCount);
                _wst.Cells[row, 5].PutValue(SumDistrictTotalCount);


                // 當有錯誤訊息
                if (ErrorData.Count > 0)
                {
                    int wstIdx = _wb.Worksheets.Add();
                    Worksheet wstErr = _wb.Worksheets[wstIdx];
                    wstErr.Name = "未分性別清單";
                    int ro = 1;
                    wstErr.Cells[0, 0].PutValue("學號");
                    wstErr.Cells[0, 1].PutValue("班級");
                    wstErr.Cells[0, 2].PutValue("座號");
                    wstErr.Cells[0, 3].PutValue("姓名");
                    wstErr.Cells[0, 4].PutValue("備註");

                    foreach (DAL.StudentEntity se in ErrorData)
                    {
                        wstErr.Cells[ro, 0].PutValue(se.StudentNumber);
                        wstErr.Cells[ro, 1].PutValue(se.ClassName);
                        wstErr.Cells[ro, 2].PutValue(se.SeatNo);
                        wstErr.Cells[ro, 3].PutValue(se.Name);
                        wstErr.Cells[ro, 4].PutValue(se.Memo);
                        ro++;
                    }
                }

                // 畫表
                Style st2 = _wb.Styles[_wb.Styles.Add()];
                StyleFlag sf2 = new StyleFlag();
                sf2.Borders = true;

                st2.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
                st2.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;
                st2.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
                st2.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
                int tmpMaxRow = 0, tmpMaxCol = 0;
                tmpMaxRow = _wst.Cells.MaxDataRow + 1;
                tmpMaxCol = _wst.Cells.MaxDataColumn + 1;
                _wst.Cells.CreateRange(0, 0, tmpMaxRow, tmpMaxCol).ApplyStyle(st2, sf2);
                for (int wstIdx = 0; wstIdx < _wb.Worksheets.Count; wstIdx++)
                {

                    tmpMaxRow = _wb.Worksheets[wstIdx].Cells.MaxDataRow + 1;
                    tmpMaxCol = _wb.Worksheets[wstIdx].Cells.MaxDataColumn + 1;
                    _wb.Worksheets[wstIdx].Cells.CreateRange(0, 0, tmpMaxRow, tmpMaxCol).ApplyStyle(st2, sf2);
                    for (int r = 0; r <= _wb.Worksheets[wstIdx].Cells.MaxDataRow; r++)
                        for (int c = 0; c <= _wb.Worksheets[wstIdx].Cells.MaxDataColumn; c++)
                            _wb.Worksheets[wstIdx].Cells[r, c].Style.Font.Size = 12;

                    _wb.Worksheets[wstIdx].AutoFitColumns();
                    _wb.Worksheets[wstIdx].AutoFitRows();
                }


            }
            #endregion


            #region 職業統計
            if (rpt == RptProcessType.職業統計)
            {
                DAL.StudentParentJobCounter StudParentJobCounter = (DAL.StudentParentJobCounter)arg[1];
                DAL.StudentParentJobCounter.ParentJobType SelectParentJobType = (DAL.StudentParentJobCounter.ParentJobType)arg[2];
                List<string> SelectJobItems = (List<string>)arg[3];
                _wb = new Workbook();
                _wst = _wb.Worksheets[0];
                List<string> wsTitle = new List<string>();
                wsTitle.Add("年級班級");
                foreach (string str in SelectJobItems)
                {
                    wsTitle.Add(str + "-人數");
                    wsTitle.Add(str + "-比例(％)");
                }
                wsTitle.Add("總人數");

                int row = 0, col = 0;
                foreach (string str in wsTitle)
                    _wst.Cells[row, col++].PutValue(str);

                row = 1;
                foreach (KeyValuePair<string, List<string>> val in StudParentJobCounter.GetSelectGradeClassName())
                {
                    int itemColIdx = 1;
                    // 處理班級                    
                    foreach (string str in val.Value)
                    {
                        itemColIdx = 1;
                        _wst.Cells[row, 0].PutValue(str);
                        for (int idx = 0; idx < SelectJobItems.Count; idx++)
                        {
                            _wst.Cells[row, itemColIdx].PutValue(StudParentJobCounter.GetParentJobCount(str, SelectJobItems[idx], SelectParentJobType, false));
                            itemColIdx++;
                            _wst.Cells[row, itemColIdx].PutValue(StudParentJobCounter.GetParentJobCountPercentage(str, SelectJobItems[idx], SelectParentJobType, false, 2));
                            itemColIdx++;
                        }

                        // 班級總人數
                        _wst.Cells[row, itemColIdx].PutValue(StudParentJobCounter.GetGradeClassStudCount(str, false));

                        row++;
                    }

                    // 處理年級
                    itemColIdx = 1;
                    _wst.Cells[row, 0].PutValue(val.Key + "年級");
                    for (int idx = 0; idx < SelectJobItems.Count; idx++)
                    {
                        _wst.Cells[row, itemColIdx].PutValue(StudParentJobCounter.GetParentJobCount(val.Key, SelectJobItems[idx], SelectParentJobType, true));
                        itemColIdx++;
                        _wst.Cells[row, itemColIdx].PutValue(StudParentJobCounter.GetParentJobCountPercentage(val.Key, SelectJobItems[idx], SelectParentJobType, true, 2));
                        itemColIdx++;
                    }
                    // 年級總人數
                    _wst.Cells[row, itemColIdx].PutValue(StudParentJobCounter.GetGradeClassStudCount(val.Key, true));

                    row++;
                }

                // 畫表
                Style st2 = _wb.Styles[_wb.Styles.Add()];
                StyleFlag sf2 = new StyleFlag();
                sf2.Borders = true;

                st2.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
                st2.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;
                st2.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
                st2.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
                int tmpMaxRow = 0, tmpMaxCol = 0;

                for (int wstIdx = 0; wstIdx < _wb.Worksheets.Count; wstIdx++)
                {

                    tmpMaxRow = _wb.Worksheets[wstIdx].Cells.MaxDataRow + 1;
                    tmpMaxCol = _wb.Worksheets[wstIdx].Cells.MaxDataColumn + 1;
                    _wb.Worksheets[wstIdx].Cells.CreateRange(0, 0, tmpMaxRow, tmpMaxCol).ApplyStyle(st2, sf2);
                    for (int r = 0; r <= _wb.Worksheets[wstIdx].Cells.MaxDataRow; r++)
                        for (int c = 0; c <= _wb.Worksheets[wstIdx].Cells.MaxDataColumn; c++)
                            _wb.Worksheets[wstIdx].Cells[r, c].Style.Font.Size = 12;

                    _wb.Worksheets[wstIdx].AutoFitColumns();
                    _wb.Worksheets[wstIdx].AutoFitRows();
                }


            }
            #endregion

            #region 年齡統計

            if (rpt == RptProcessType.年齡統計)
            {
                DAL.StudentAgeCounter sacTer = (DAL.StudentAgeCounter)arg[1];
                List<DAL.StudentEntity> ErrorData = (List<DAL.StudentEntity>)arg[2];
                _wb = new Workbook();
                _wst = _wb.Worksheets[0];
                List<string> wsTitle = new List<string>();
                wsTitle.Add("年級");
                wsTitle.Add("年齡");
                wsTitle.Add("男生數");
                wsTitle.Add("男生比(%)");
                wsTitle.Add("女生數");
                wsTitle.Add("女生比(%)");
                wsTitle.Add("未分性別人數");
                wsTitle.Add("總人數");

                int row = 0, col = 0;
                foreach (string str in wsTitle)
                {
                    _wst.Cells[row, col].PutValue(str);
                    col++;
                }

                row = 1;
                int BoyCount = 0, GirlCount = 0, TotalCount = 0, SumBoyCount = 0, SumGrilCount = 0, SumTotalCount = 0, SumBoyCountByGradeYear = 0, SumGirlCountByGradeYear = 0, SumTotalCountByGradeYear = 0, NoGenderCount = 0, SumNoGenderCount = 0, SumNoGenderCountByGradeYear = 0;
                for (int i = 1; i <= 3; i++)
                {
                    SumBoyCountByGradeYear = 0;
                    SumGirlCountByGradeYear = 0;
                    SumTotalCountByGradeYear = 0;

                    _wst.Cells.CreateRange(row, 0, sacTer.wsItems.Count, 1).Merge();
                    _wst.Cells[row, 0].PutValue(i + "年級");
                    foreach (string str in sacTer.wsItems)
                    {
                        _wst.Cells[row, 1].PutValue(str);

                        TotalCount = sacTer.GetAgeCounterItemValue(str, JHSchool.Permrec.ClassExtendControls.Reports.DAL.StudentAgeCounter.AgeCountType.全部, i.ToString());

                        BoyCount = sacTer.GetAgeCounterItemValue(str, JHSchool.Permrec.ClassExtendControls.Reports.DAL.StudentAgeCounter.AgeCountType.男生, i.ToString());
                        if (BoyCount > 0)
                        {
                            _wst.Cells[row, 2].PutValue(BoyCount);

                            if (TotalCount > 0)
                                _wst.Cells[row, 3].PutValue(sacTer.IntRoundToDecimalForPercentage(BoyCount, TotalCount, 2));
                        }

                        GirlCount = sacTer.GetAgeCounterItemValue(str, JHSchool.Permrec.ClassExtendControls.Reports.DAL.StudentAgeCounter.AgeCountType.女生, i.ToString());
                        if (GirlCount > 0)
                        {
                            _wst.Cells[row, 4].PutValue(GirlCount);

                            if (TotalCount > 0)
                                _wst.Cells[row, 5].PutValue(sacTer.IntRoundToDecimalForPercentage(GirlCount, TotalCount, 2));

                        }

                        // 未分性別
                        NoGenderCount = sacTer.GetAgeCounterItemValue(str, JHSchool.Permrec.ClassExtendControls.Reports.DAL.StudentAgeCounter.AgeCountType.未分性別, i.ToString());
                        if (NoGenderCount > 0)
                        {
                            _wst.Cells[row, 6].PutValue(NoGenderCount);

                        }

                        // 總人數
                        if (TotalCount > 0)
                        {
                            _wst.Cells[row, 7].PutValue(TotalCount);
                            SumTotalCountByGradeYear += TotalCount;
                        }

                        // 累加
                        SumBoyCountByGradeYear += BoyCount;
                        SumGirlCountByGradeYear += GirlCount;
                        SumNoGenderCountByGradeYear += NoGenderCount;

                        row++;
                    }

                    // 處理小計
                    _wst.Cells[row, 0].PutValue("小計");
                    if (SumBoyCountByGradeYear > 0)
                    {
                        _wst.Cells[row, 2].PutValue(SumBoyCountByGradeYear);

                        if (SumTotalCountByGradeYear > 0)
                            _wst.Cells[row, 3].PutValue(sacTer.IntRoundToDecimalForPercentage(SumBoyCountByGradeYear, SumTotalCountByGradeYear, 2));
                    }

                    if (SumGirlCountByGradeYear > 0)
                    {
                        _wst.Cells[row, 4].PutValue(SumGirlCountByGradeYear);

                        if (SumTotalCountByGradeYear > 0)
                            _wst.Cells[row, 5].PutValue(sacTer.IntRoundToDecimalForPercentage(SumGirlCountByGradeYear, SumTotalCountByGradeYear, 2));
                    }

                    if (SumNoGenderCountByGradeYear > 0)
                        _wst.Cells[row, 6].PutValue(SumNoGenderCountByGradeYear);

                    if (SumTotalCountByGradeYear > 0)
                        _wst.Cells[row, 7].PutValue(SumTotalCountByGradeYear);

                    SumBoyCount += SumBoyCountByGradeYear;
                    SumGrilCount += SumGirlCountByGradeYear;
                    SumTotalCount += SumTotalCountByGradeYear;
                    SumNoGenderCount += SumNoGenderCountByGradeYear;
                    row++;
                }
                _wst.Cells[row, 0].PutValue("總計");

                // 處理總計
                if (SumBoyCount > 0)
                {
                    _wst.Cells[row, 2].PutValue(SumBoyCount);

                    if (SumTotalCount > 0)
                        _wst.Cells[row, 3].PutValue(sacTer.IntRoundToDecimalForPercentage(SumBoyCount, SumTotalCount, 2));
                }

                if (SumGrilCount > 0)
                {
                    _wst.Cells[row, 4].PutValue(SumGrilCount);

                    if (SumTotalCount > 0)
                        _wst.Cells[row, 5].PutValue(sacTer.IntRoundToDecimalForPercentage(SumGrilCount, SumTotalCount, 2));
                }

                if (SumNoGenderCount > 0)
                    _wst.Cells[row, 6].PutValue(SumNoGenderCount);

                if (SumTotalCount > 0)
                    _wst.Cells[row, 7].PutValue(SumTotalCount);


                // 當有錯誤訊息
                if (ErrorData.Count > 0)
                {
                    int wstIdx = _wb.Worksheets.Add();
                    Worksheet wstErr = _wb.Worksheets[wstIdx];
                    wstErr.Name = "未分性別清單";
                    int ro = 1;
                    wstErr.Cells[0, 0].PutValue("學號");
                    wstErr.Cells[0, 1].PutValue("班級");
                    wstErr.Cells[0, 2].PutValue("座號");
                    wstErr.Cells[0, 3].PutValue("姓名");
                    wstErr.Cells[0, 4].PutValue("備註");

                    foreach (DAL.StudentEntity se in ErrorData)
                    {
                        wstErr.Cells[ro, 0].PutValue(se.StudentNumber);
                        wstErr.Cells[ro, 1].PutValue(se.ClassName);
                        wstErr.Cells[ro, 2].PutValue(se.SeatNo);
                        wstErr.Cells[ro, 3].PutValue(se.Name);
                        wstErr.Cells[ro, 4].PutValue(se.Memo);
                        ro++;
                    }
                }

                // 畫表
                Style st2 = _wb.Styles[_wb.Styles.Add()];
                StyleFlag sf2 = new StyleFlag();
                sf2.Borders = true;

                st2.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
                st2.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;
                st2.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
                st2.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
                int tmpMaxRow = 0, tmpMaxCol = 0;

                for (int wstIdx = 0; wstIdx < _wb.Worksheets.Count; wstIdx++)
                {

                    tmpMaxRow = _wb.Worksheets[wstIdx].Cells.MaxDataRow + 1;
                    tmpMaxCol = _wb.Worksheets[wstIdx].Cells.MaxDataColumn + 1;
                    _wb.Worksheets[wstIdx].Cells.CreateRange(0, 0, tmpMaxRow, tmpMaxCol).ApplyStyle(st2, sf2);
                    for (int r = 0; r <= _wb.Worksheets[wstIdx].Cells.MaxDataRow; r++)
                        for (int c = 0; c <= _wb.Worksheets[wstIdx].Cells.MaxDataColumn; c++)
                            _wb.Worksheets[wstIdx].Cells[r, c].Style.Font.Size = 12;

                    _wb.Worksheets[wstIdx].AutoFitColumns();
                    _wb.Worksheets[wstIdx].AutoFitRows();
                }



            }
            #endregion

            #region 類別統計

            if (rpt == RptProcessType.類別統計)
            {
                _wb = new Workbook();
                _wst = _wb.Worksheets[0];
                _wst.Name = _WorksheetName;
                DAL.StudentTagCounter stc = (DAL.StudentTagCounter)arg[1];

                int wstRow = 0, wstCol = 2;

                _wst.Cells[wstRow, 0].PutValue("班級");
                _wst.Cells[wstRow, 1].PutValue("性別");

                foreach (string Items in stc.GetSelectedItems())
                {
                    _wst.Cells[wstRow, wstCol].PutValue(Items);
                    wstCol++;
                }

                // 讀取年級後排序
                List<string> GradeYearList = new List<string>();

                foreach (KeyValuePair<string, List<string>> gradeyear in stc.GetSelectedGradeClassNames())
                {
                    if (!GradeYearList.Contains(gradeyear.Key))
                        GradeYearList.Add(gradeyear.Key);
                }

                GradeYearList.Sort();

                Dictionary<string, int> TotalCountDic = new Dictionary<string, int>();
                foreach (string str in stc.GetSelectedItems())
                {
                    if (!TotalCountDic.ContainsKey(str))
                        TotalCountDic.Add(str, 0);
                }

                wstRow++;
                // 報表呈現
                foreach (string strGradeYear in GradeYearList)
                {

                    foreach (string ClassName in stc.GetSelectedGradeClassNames()[strGradeYear])
                    {
                        _wst.Cells.CreateRange(wstRow, 0, 3, 1).Merge();
                        _wst.Cells[wstRow, 0].PutValue(ClassName);
                        _wst.Cells[wstRow, 1].PutValue("男");
                        wstCol = 2;
                        foreach (string ItemName in stc.GetSelectedItems())
                        {
                            int BoyCount = stc.GetStudentTagCount(ClassName, ItemName, false, JHSchool.Permrec.ClassExtendControls.Reports.DAL.StudentTagCounter.GenderType.男生);
                            _wst.Cells[wstRow, wstCol].PutValue(BoyCount);
                            wstCol++;
                        }
                        wstRow++;

                        _wst.Cells[wstRow, 1].PutValue("女");
                        wstCol = 2;
                        foreach (string ItemName in stc.GetSelectedItems())
                        {
                            int GirlCount = stc.GetStudentTagCount(ClassName, ItemName, false, JHSchool.Permrec.ClassExtendControls.Reports.DAL.StudentTagCounter.GenderType.女生);
                            _wst.Cells[wstRow, wstCol].PutValue(GirlCount);
                            wstCol++;
                        }
                        wstRow++;

                        _wst.Cells[wstRow, 1].PutValue("小計");
                        wstCol = 2;
                        foreach (string ItemName in stc.GetSelectedItems())
                        {
                            int TotalCount = stc.GetStudentTagCount(ClassName, ItemName, false, JHSchool.Permrec.ClassExtendControls.Reports.DAL.StudentTagCounter.GenderType.全部);
                            _wst.Cells[wstRow, wstCol].PutValue(TotalCount);
                            wstCol++;
                        }
                        wstRow++;
                    }

                    // 年級
                    _wst.Cells[wstRow, 0].PutValue(strGradeYear + "年級");
                    wstCol = 2;
                    foreach (string ItemName in stc.GetSelectedItems())
                    {
                        int TotalCount = stc.GetStudentTagCount(strGradeYear, ItemName, true, JHSchool.Permrec.ClassExtendControls.Reports.DAL.StudentTagCounter.GenderType.全部);
                        _wst.Cells[wstRow, wstCol].PutValue(TotalCount);
                        wstCol++;
                        if (TotalCountDic.ContainsKey(ItemName))
                            TotalCountDic[ItemName] += TotalCount;
                    }
                    wstRow++;
                }
                _wst.Cells[wstRow, 0].PutValue("總計");
                wstCol = 2;
                // 總計

                foreach (int val in TotalCountDic.Values)
                {
                    _wst.Cells[wstRow, wstCol].PutValue(val);
                    wstCol++;
                }

                // 畫表
                Style st2 = _wb.Styles[_wb.Styles.Add()];
                StyleFlag sf2 = new StyleFlag();
                sf2.Borders = true;

                st2.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
                st2.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;
                st2.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
                st2.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
                int tmpMaxRow = 0, tmpMaxCol = 0;

                for (int wstIdx = 0; wstIdx < _wb.Worksheets.Count; wstIdx++)
                {

                    tmpMaxRow = _wb.Worksheets[wstIdx].Cells.MaxDataRow + 1;
                    tmpMaxCol = _wb.Worksheets[wstIdx].Cells.MaxDataColumn + 1;
                    _wb.Worksheets[wstIdx].Cells.CreateRange(0, 0, tmpMaxRow, tmpMaxCol).ApplyStyle(st2, sf2);
                    for (int r = 0; r <= _wb.Worksheets[wstIdx].Cells.MaxDataRow; r++)
                        for (int c = 0; c <= _wb.Worksheets[wstIdx].Cells.MaxDataColumn; c++)
                            _wb.Worksheets[wstIdx].Cells[r, c].Style.Font.Size = 12;

                    _wb.Worksheets[wstIdx].AutoFitColumns();
                    _wb.Worksheets[wstIdx].AutoFitRows();
                }
            }
            #endregion
            e.Result = _wb;
        }

    }
}

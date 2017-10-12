using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using NLog;
using SailorDomain.Entities;
using OverseeService.Services;
using System.Data.Entity;

namespace OverseeService
{
    public partial class Oversee : ServiceBase
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public Timer myTimer;
        public Oversee()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            myTimer = new Timer();
            //启动之后先执行一次，然后定时执行
            //DoOversee();
            //执行间隔,单位：毫秒    此处设置为1小时
            //                 毫秒   秒   分  小时
            //myTimer.Interval = 1000 * 60;
            myTimer.Interval = 1000 * 60 * 60 * 1;
            myTimer.Elapsed += CheckIsInTime;
            myTimer.Enabled = true;
            myTimer.Start();

            logger.Info("扫描服务Start");
        }

        protected override void OnStop()
        {
            myTimer.Enabled = false;
            myTimer.Stop();

            logger.Info("扫描服务Stop");
        }

        protected void CheckIsInTime(object sender, EventArgs e)
        {
            logger.Info("执行间隔模块");
            //if(true)
            if (DateTime.Now.Hour == 4)
            {
                logger.Info("正式操作Start");
                SailorAboard();
                //SailorAshore();
                SailorBeginTraining();
                SailorEndTraining();
                logger.Info("正式操作End");
            }
        }

        protected void SailorAboard()
        {
            logger.Info("船员上船操作Start");
            DateTime today = DateTime.Now.Date;
            var sailorService = new SailorService();
            var contractService = new ContractService();
            var recordService = new ServiceRecordService();
            var contracts = contractService.GetEntities().Where(c => c.AboardDate == today).ToList();
            foreach (var contract in contracts)
            {
                try
                {
                    var record = recordService.Find(s => s.ContractID == contract.ContractID);
                    if (record != null)
                    {
                        var sailor = sailorService.Find(contract.SailorID);
                        if (sailor != null)
                        {
                            sailor.Status = SailorStatus.在船;
                            sailor.VesselID = contract.VesselID;
                            sailor.VesselName = contract.VesselName;
                            sailor.ServiceRecordID = record.ServiceRecordID;
                            if (sailorService.Update(sailor))
                                logger.Info("船员：" + sailor.Name + "(" + sailor.SailorID + ")" + "已上船：" + sailor.VesselName);
                            else
                                logger.Info("船员：" + sailor.Name + "(" + sailor.SailorID + ")" + "上船失败");
                        }
                    }
                    
                }
                catch (Exception ex)
                {
                    logger.Error(ex);
                }
            }
            logger.Info("船员上船操作End");
        }

        protected void SailorAshore()
        {
            logger.Info("船员下船操作Start");
            DateTime yestoday=DateTime.Now.Date.AddDays(-1);
            var sailorService = new SailorService();
            var contractService = new ContractService();
            var recordService=new ServiceRecordService();
            var contracts = contractService.GetEntities().Where(c => c.AshoreDate == yestoday).ToList();
            foreach (var contract in contracts)
            {
                try
                {
                    var record = recordService.Find(r => r.ContractID == contract.ContractID);
                    if (record != null)
                    {
                        var sailor = sailorService.Find(s => s.SailorID == contract.SailorID && s.Status == SailorStatus.在船 && s.ServiceRecordID == record.ServiceRecordID);
                        if (sailor!=null)
                        {
                            sailor.Status = SailorStatus.休假;
                            sailor.VesselID = null;
                            sailor.VesselName = "";
                            sailor.ServiceRecordID = null;
                            if (sailorService.Update(sailor))
                                logger.Info("船员：" + sailor.Name + "(" + sailor.SailorID + ")" + "已下船：" + sailor.VesselName);
                            else
                                logger.Info("船员：" + sailor.Name + "(" + sailor.SailorID + ")" + "参加培训失败");
                        }
                    }
                }
                catch (Exception ex)
                {
                    logger.Error(ex);
                }
            }
            logger.Info("船员下船操作End");
        }

        protected void SailorBeginTraining()
        {
            logger.Info("船员参加培训操作Start");
            DateTime today = DateTime.Now.Date;
            var sailorService = new SailorService();
            var trainingclassService = new TrainingClassService();

            var trainingClasses = trainingclassService.GetEntities().Include(x => x.Trainees).Where(x => x.BeginDate == today).ToList();
            foreach (var training in trainingClasses)
            {
                foreach (var trainee in training.Trainees)
                {
                    try
                    {
                        var sailor = sailorService.Find(s => s.SailorID == trainee.SailorID && s.Status != SailorStatus.在船);
                        if (sailor != null)
                        {
                            sailor.Status = SailorStatus.培训;
                            sailor.TraineeID = trainee.TraineeID;
                            if (sailorService.Update(sailor))
                                logger.Info("船员：" + sailor.Name + "(" + sailor.SailorID + ")" + "参加培训：" + training.Name);
                            else
                                logger.Info("船员：" + sailor.Name + "(" + sailor.SailorID + ")" + "参加培训失败");
                        }
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex);
                    }
                }
            }
            logger.Info("船员参加培训操作End");
        }

        protected void SailorEndTraining()
        {
            logger.Info("船员结束培训操作Start");
            DateTime yestoday = DateTime.Now.Date.AddDays(-1);
            var sailorService = new SailorService();
            var trainingclassService = new TrainingClassService();

            var trainingClasses = trainingclassService.GetEntities().Include(x => x.Trainees).Where(x => x.EndDate == yestoday).ToList();
            foreach (var training in trainingClasses)
            {
                foreach (var trainee in training.Trainees)
                {
                    try
                    {
                        var sailor = sailorService.Find(s=>s.SailorID==trainee.SailorID&&s.Status == SailorStatus.培训 && s.TraineeID == trainee.TraineeID);
                        if (sailor!=null)
                        {
                            sailor.Status = SailorStatus.待派;
                            sailor.TraineeID = null;
                            if (sailorService.Update(sailor))
                                logger.Info("船员：" + sailor.Name + "(" + sailor.SailorID + ")" + "结束培训：" + training.Name);
                            else
                                logger.Info("船员：" + sailor.Name + "(" + sailor.SailorID + ")" + "结束培训失败");
                        }
                        
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex);
                    }
                    
                }
            }
            logger.Info("船员结束培训操作End");
        }
    }
}

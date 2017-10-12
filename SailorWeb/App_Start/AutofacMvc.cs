using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using System.Reflection;
using SailorWeb.Services;

namespace SailorWeb
{
    public class AutofacMvc
    {
        public static void Initialize()
        {
            DependencyResolver.SetResolver(new AutofacDependencyResolver(RegisterServices(new ContainerBuilder())));
        }

        private static IContainer RegisterServices(ContainerBuilder builder)
        {

            builder.RegisterControllers(Assembly.GetExecutingAssembly());

            // Services
            builder.RegisterType<CertificateService>()
                .As<ICertificateService>()
                .InstancePerRequest();
            builder.RegisterType<CertificateTypeService>()
                .As<ICertificateTypeService>()
                .InstancePerRequest();
            builder.RegisterType<CompanyService>()
                .As<ICompanyService>()
                .InstancePerRequest();
            builder.RegisterType<ContractService>()
                .As<IContractService>()
                .InstancePerRequest();
            builder.RegisterType<ExamService>()
                .As<IExamService>()
                .InstancePerRequest();
            builder.RegisterType<ExperienceService>()
                .As<IExperienceService>()
                .InstancePerRequest();
            builder.RegisterType<FamilyService>()
                .As<IFamilyService>()
                .InstancePerRequest();
            builder.RegisterType<InterviewService>()
                .As<IInterviewService>()
                .InstancePerRequest();
            builder.RegisterType<LaborSupplyService>()
                .As<ILaborSupplyService>()
                .InstancePerRequest();
            builder.RegisterType<NoticeService>()
                .As<INoticeService>()
                .InstancePerRequest();
            builder.RegisterType<SailorService>()
                .As<ISailorService>()
                .InstancePerRequest();
            builder.RegisterType<ServiceRecordService>()
                .As<IServiceRecordService>()
                .InstancePerRequest();
            builder.RegisterType<ShipownerService>()
                .As<IShipownerService>()
                .InstancePerRequest();
            builder.RegisterType<TitleService>()
                .As<ITitleService>()
                .InstancePerRequest();
            builder.RegisterType<TrainingClassService>()
                .As<ITrainingClassService>()
                .InstancePerRequest();
            builder.RegisterType<TraineeService>()
                .As<ITraineeService>()
                .InstancePerRequest();
            builder.RegisterType<UploadFileService>()
                .As<IUploadFileService>()
                .InstancePerRequest();
            builder.RegisterType<VesselAccountService>()
                .As<IVesselAccountService>()
                .InstancePerRequest();
            builder.RegisterType<VesselCertificateService>()
                .As<IVesselCertificateService>()
                .InstancePerRequest();
            builder.RegisterType<VesselService>()
                .As<IVesselService>()
                .InstancePerRequest();
            builder.RegisterType<SysCompanyService>()
                .As<ISysCompanyService>()
                .InstancePerRequest();
            builder.RegisterType<WageService>()
                .As<IWageService>()
                .InstancePerRequest();

            return builder.Build();
        }
    }
}
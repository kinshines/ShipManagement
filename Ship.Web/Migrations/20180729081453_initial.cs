using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Ship.Web.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CertificateType",
                columns: table => new
                {
                    SysUserId = table.Column<string>(maxLength: 50, nullable: true),
                    SysCompanyId = table.Column<int>(nullable: false),
                    CertificateTypeID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Description = table.Column<string>(maxLength: 100, nullable: true),
                    CertificateCategory = table.Column<byte>(nullable: false),
                    IsPublic = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CertificateType", x => x.CertificateTypeID);
                });

            migrationBuilder.CreateTable(
                name: "Company",
                columns: table => new
                {
                    SysUserId = table.Column<string>(maxLength: 50, nullable: true),
                    SysCompanyId = table.Column<int>(nullable: false),
                    CompanyID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    Type = table.Column<byte>(nullable: true),
                    Code = table.Column<string>(maxLength: 20, nullable: true),
                    Property = table.Column<byte>(nullable: true),
                    Representative = table.Column<string>(maxLength: 10, nullable: true),
                    HonorLevel = table.Column<byte>(nullable: true),
                    Address = table.Column<string>(maxLength: 200, nullable: true),
                    PostalCode = table.Column<string>(maxLength: 10, nullable: true),
                    Telephone = table.Column<string>(maxLength: 20, nullable: true),
                    Fax = table.Column<string>(maxLength: 20, nullable: true),
                    Email = table.Column<string>(maxLength: 20, nullable: true),
                    Website = table.Column<string>(maxLength: 200, nullable: true),
                    Contacter = table.Column<string>(maxLength: 20, nullable: true),
                    ContactTel = table.Column<string>(maxLength: 20, nullable: true),
                    TaxNo = table.Column<string>(maxLength: 50, nullable: true),
                    BankAccount = table.Column<string>(maxLength: 100, nullable: true),
                    Bank = table.Column<string>(maxLength: 100, nullable: true),
                    Remark = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company", x => x.CompanyID);
                });

            migrationBuilder.CreateTable(
                name: "LaborSupply",
                columns: table => new
                {
                    SysUserId = table.Column<string>(maxLength: 50, nullable: true),
                    SysCompanyId = table.Column<int>(nullable: false),
                    LaborSupplyID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 20, nullable: false),
                    Specification = table.Column<string>(maxLength: 20, nullable: true),
                    Total = table.Column<int>(nullable: false),
                    Baseline = table.Column<int>(nullable: false),
                    Remark = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LaborSupply", x => x.LaborSupplyID);
                });

            migrationBuilder.CreateTable(
                name: "Notice",
                columns: table => new
                {
                    SysUserId = table.Column<string>(maxLength: 50, nullable: true),
                    SysCompanyId = table.Column<int>(nullable: false),
                    NoticeID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Source = table.Column<byte>(nullable: false),
                    SourceID = table.Column<int>(nullable: false),
                    NoticeTime = table.Column<DateTime>(nullable: false),
                    Deadline = table.Column<DateTime>(nullable: false),
                    Content = table.Column<string>(maxLength: 500, nullable: false),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notice", x => x.NoticeID);
                });

            migrationBuilder.CreateTable(
                name: "Shipowner",
                columns: table => new
                {
                    SysUserId = table.Column<string>(maxLength: 50, nullable: true),
                    SysCompanyId = table.Column<int>(nullable: false),
                    ShipownerID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Contacter = table.Column<string>(maxLength: 50, nullable: true),
                    Address = table.Column<string>(maxLength: 50, nullable: true),
                    Telephone = table.Column<string>(maxLength: 20, nullable: true),
                    Fax = table.Column<string>(maxLength: 20, nullable: true),
                    Email = table.Column<string>(maxLength: 20, nullable: true),
                    Website = table.Column<string>(maxLength: 50, nullable: true),
                    Representative = table.Column<string>(maxLength: 10, nullable: true),
                    PostalCode = table.Column<string>(maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shipowner", x => x.ShipownerID);
                });

            migrationBuilder.CreateTable(
                name: "SysCompany",
                columns: table => new
                {
                    SysCompanyId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: true),
                    Telephone = table.Column<string>(maxLength: 20, nullable: true),
                    Contacter = table.Column<string>(maxLength: 10, nullable: true),
                    OpenTime = table.Column<DateTime>(nullable: false),
                    ExpireTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysCompany", x => x.SysCompanyId);
                });

            migrationBuilder.CreateTable(
                name: "TrainingClass",
                columns: table => new
                {
                    SysUserId = table.Column<string>(maxLength: 50, nullable: true),
                    SysCompanyId = table.Column<int>(nullable: false),
                    TrainingClassID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Subject = table.Column<string>(maxLength: 50, nullable: true),
                    BeginDate = table.Column<DateTime>(nullable: true),
                    EndDate = table.Column<DateTime>(nullable: true),
                    Period = table.Column<int>(nullable: true),
                    ClassHour = table.Column<int>(nullable: true),
                    Form = table.Column<string>(maxLength: 20, nullable: true),
                    Target = table.Column<string>(maxLength: 20, nullable: true),
                    Property = table.Column<string>(maxLength: 20, nullable: true),
                    ParticipantNumber = table.Column<int>(nullable: true),
                    GraduateNumber = table.Column<int>(nullable: true),
                    SchoolingLength = table.Column<string>(maxLength: 20, nullable: true),
                    EducationDegree = table.Column<string>(maxLength: 20, nullable: true),
                    Teacher = table.Column<string>(maxLength: 20, nullable: true),
                    Company = table.Column<string>(maxLength: 50, nullable: true),
                    Fees = table.Column<double>(nullable: true),
                    Remark = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingClass", x => x.TrainingClassID);
                });

            migrationBuilder.CreateTable(
                name: "UploadFile",
                columns: table => new
                {
                    SysUserId = table.Column<string>(maxLength: 50, nullable: true),
                    SysCompanyId = table.Column<int>(nullable: false),
                    UploadFileID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 200, nullable: true),
                    Path = table.Column<string>(maxLength: 100, nullable: true),
                    CreateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UploadFile", x => x.UploadFileID);
                });

            migrationBuilder.CreateTable(
                name: "LaborSupplyPut",
                columns: table => new
                {
                    LaborSupplyPutID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PutType = table.Column<byte>(nullable: false),
                    PutDate = table.Column<DateTime>(nullable: false),
                    Amount = table.Column<int>(nullable: false),
                    Remark = table.Column<string>(maxLength: 100, nullable: true),
                    LaborSupplyID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LaborSupplyPut", x => x.LaborSupplyPutID);
                    table.ForeignKey(
                        name: "FK_LaborSupplyPut_LaborSupply_LaborSupplyID",
                        column: x => x.LaborSupplyID,
                        principalTable: "LaborSupply",
                        principalColumn: "LaborSupplyID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LaborSupplyTake",
                columns: table => new
                {
                    LaborSupplyTakeID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TakeType = table.Column<byte>(nullable: false),
                    TakeDate = table.Column<DateTime>(nullable: false),
                    Department = table.Column<string>(maxLength: 50, nullable: true),
                    TakePerson = table.Column<string>(maxLength: 10, nullable: true),
                    Amount = table.Column<int>(nullable: false),
                    Remark = table.Column<string>(maxLength: 100, nullable: true),
                    LaborSupplyID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LaborSupplyTake", x => x.LaborSupplyTakeID);
                    table.ForeignKey(
                        name: "FK_LaborSupplyTake_LaborSupply_LaborSupplyID",
                        column: x => x.LaborSupplyID,
                        principalTable: "LaborSupply",
                        principalColumn: "LaborSupplyID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Vessel",
                columns: table => new
                {
                    SysUserId = table.Column<string>(maxLength: 50, nullable: true),
                    SysCompanyId = table.Column<int>(nullable: false),
                    VesselID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Catchword = table.Column<string>(maxLength: 20, nullable: true),
                    Type = table.Column<byte>(nullable: true),
                    Flag = table.Column<string>(maxLength: 50, nullable: true),
                    SailZone = table.Column<string>(maxLength: 50, nullable: true),
                    GrossTon = table.Column<int>(nullable: true),
                    DeadWeightTon = table.Column<int>(nullable: true),
                    NetTon = table.Column<int>(nullable: true),
                    Power = table.Column<string>(maxLength: 10, nullable: true),
                    BuildDate = table.Column<DateTime>(nullable: true),
                    BuildPlace = table.Column<string>(maxLength: 200, nullable: true),
                    MainEngine = table.Column<string>(maxLength: 200, nullable: true),
                    AuxiliaryEngine = table.Column<string>(maxLength: 200, nullable: true),
                    ElectricGenerator = table.Column<string>(maxLength: 20, nullable: true),
                    MinManning = table.Column<int>(nullable: true),
                    Manager = table.Column<string>(maxLength: 20, nullable: true),
                    ShipownerID = table.Column<int>(nullable: false),
                    ShipownerName = table.Column<string>(maxLength: 50, nullable: true),
                    IMO = table.Column<string>(maxLength: 20, nullable: true),
                    VesselManageType = table.Column<byte>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vessel", x => x.VesselID);
                    table.ForeignKey(
                        name: "FK_Vessel_Shipowner_ShipownerID",
                        column: x => x.ShipownerID,
                        principalTable: "Shipowner",
                        principalColumn: "ShipownerID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Sailor",
                columns: table => new
                {
                    SysUserId = table.Column<string>(maxLength: 50, nullable: true),
                    SysCompanyId = table.Column<int>(nullable: false),
                    SailorID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 10, nullable: false),
                    EnglishName = table.Column<string>(maxLength: 50, nullable: true),
                    Post = table.Column<byte>(nullable: false),
                    IdentityNo = table.Column<string>(maxLength: 50, nullable: false),
                    Birthday = table.Column<DateTime>(nullable: false),
                    Birthplace = table.Column<string>(maxLength: 50, nullable: false),
                    Gender = table.Column<byte>(nullable: false),
                    Ethnic = table.Column<byte>(nullable: true),
                    EnglishLevel = table.Column<byte>(nullable: true),
                    Marital = table.Column<byte>(nullable: false),
                    Source = table.Column<byte>(nullable: true),
                    Degree = table.Column<byte>(nullable: true),
                    WorkInDate = table.Column<DateTime>(nullable: true),
                    ComeInDate = table.Column<DateTime>(nullable: true),
                    EducationDegree = table.Column<byte>(nullable: true),
                    Major = table.Column<string>(maxLength: 50, nullable: true),
                    GraduateCollege = table.Column<string>(maxLength: 50, nullable: true),
                    EnrollmentDate = table.Column<DateTime>(nullable: true),
                    GraduateDate = table.Column<DateTime>(nullable: true),
                    Mobile = table.Column<string>(maxLength: 20, nullable: true),
                    HomeContacter = table.Column<string>(maxLength: 20, nullable: true),
                    HomeTel = table.Column<string>(maxLength: 20, nullable: true),
                    Address = table.Column<string>(maxLength: 200, nullable: true),
                    Height = table.Column<int>(nullable: true),
                    Weight = table.Column<int>(nullable: true),
                    Blood = table.Column<byte>(nullable: false),
                    Sleeve = table.Column<string>(maxLength: 10, nullable: true),
                    Collar = table.Column<string>(maxLength: 10, nullable: true),
                    Waist = table.Column<string>(maxLength: 10, nullable: true),
                    Chest = table.Column<string>(maxLength: 10, nullable: true),
                    CoatLength = table.Column<string>(maxLength: 10, nullable: true),
                    TrouserLength = table.Column<string>(maxLength: 10, nullable: true),
                    ShoeSize = table.Column<string>(maxLength: 10, nullable: true),
                    HatSize = table.Column<string>(maxLength: 10, nullable: true),
                    WageCardNo = table.Column<string>(maxLength: 50, nullable: true),
                    AccountName = table.Column<string>(maxLength: 10, nullable: true),
                    Bank = table.Column<string>(maxLength: 50, nullable: true),
                    ProvidentFundNo = table.Column<string>(maxLength: 50, nullable: true),
                    PensionNo = table.Column<string>(maxLength: 50, nullable: true),
                    UnemployBenefitNo = table.Column<string>(maxLength: 50, nullable: true),
                    MedicalInsuranceNo = table.Column<string>(maxLength: 50, nullable: true),
                    SocialInsuranceNo = table.Column<string>(maxLength: 50, nullable: true),
                    Email = table.Column<string>(maxLength: 20, nullable: true),
                    Remark = table.Column<string>(maxLength: 500, nullable: true),
                    Status = table.Column<byte>(nullable: false),
                    VesselID = table.Column<int>(nullable: true),
                    VesselName = table.Column<string>(nullable: true),
                    FileID = table.Column<int>(nullable: true),
                    ServiceRecordID = table.Column<int>(nullable: true),
                    TraineeID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sailor", x => x.SailorID);
                    table.ForeignKey(
                        name: "FK_Sailor_Vessel_VesselID",
                        column: x => x.VesselID,
                        principalTable: "Vessel",
                        principalColumn: "VesselID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VesselAccount",
                columns: table => new
                {
                    SysUserId = table.Column<string>(maxLength: 50, nullable: true),
                    SysCompanyId = table.Column<int>(nullable: false),
                    VesselAccountID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FeeItem = table.Column<byte>(nullable: false),
                    Side = table.Column<byte>(nullable: false),
                    Cost = table.Column<double>(nullable: true),
                    USCost = table.Column<double>(nullable: true),
                    Deposit = table.Column<double>(nullable: true),
                    USDeposit = table.Column<double>(nullable: true),
                    Payoff = table.Column<bool>(nullable: false),
                    Payment = table.Column<double>(nullable: true),
                    USPayment = table.Column<double>(nullable: true),
                    Debt = table.Column<double>(nullable: true),
                    USDebt = table.Column<double>(nullable: true),
                    Balance = table.Column<double>(nullable: false),
                    USBalance = table.Column<double>(nullable: false),
                    InvoiceDate = table.Column<DateTime>(nullable: false),
                    PaymentDate = table.Column<DateTime>(nullable: true),
                    Remark = table.Column<string>(maxLength: 500, nullable: true),
                    VesselID = table.Column<int>(nullable: false),
                    VesselName = table.Column<string>(maxLength: 50, nullable: true),
                    ShipownerID = table.Column<int>(nullable: false),
                    ShipownerName = table.Column<string>(maxLength: 50, nullable: true),
                    CompanyID = table.Column<int>(nullable: true),
                    CompanyName = table.Column<string>(maxLength: 200, nullable: true),
                    InvoiceNo = table.Column<string>(maxLength: 50, nullable: true),
                    InvoiceFileID = table.Column<int>(nullable: true),
                    SignFileID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VesselAccount", x => x.VesselAccountID);
                    table.ForeignKey(
                        name: "FK_VesselAccount_Company_CompanyID",
                        column: x => x.CompanyID,
                        principalTable: "Company",
                        principalColumn: "CompanyID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VesselAccount_Vessel_VesselID",
                        column: x => x.VesselID,
                        principalTable: "Vessel",
                        principalColumn: "VesselID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VesselBalance",
                columns: table => new
                {
                    VesselID = table.Column<int>(nullable: false),
                    Balance = table.Column<double>(nullable: false),
                    USBalance = table.Column<double>(nullable: false),
                    ShipownerID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VesselBalance", x => x.VesselID);
                    table.ForeignKey(
                        name: "FK_VesselBalance_Shipowner_ShipownerID",
                        column: x => x.ShipownerID,
                        principalTable: "Shipowner",
                        principalColumn: "ShipownerID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VesselBalance_Vessel_VesselID",
                        column: x => x.VesselID,
                        principalTable: "Vessel",
                        principalColumn: "VesselID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VesselCertificate",
                columns: table => new
                {
                    SysUserId = table.Column<string>(maxLength: 50, nullable: true),
                    SysCompanyId = table.Column<int>(nullable: false),
                    VesselCertificateID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: true),
                    Code = table.Column<string>(maxLength: 50, nullable: true),
                    IssueDate = table.Column<DateTime>(nullable: true),
                    ExpiryDate = table.Column<DateTime>(nullable: true),
                    CheckBeginDate = table.Column<DateTime>(nullable: true),
                    CheckEndDate = table.Column<DateTime>(nullable: true),
                    CheckNoticeDate = table.Column<DateTime>(nullable: true),
                    ExpiryNoticeDate = table.Column<DateTime>(nullable: true),
                    VesselID = table.Column<int>(nullable: false),
                    VesselName = table.Column<string>(maxLength: 50, nullable: true),
                    FileID = table.Column<int>(nullable: true),
                    CertificateTypeID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VesselCertificate", x => x.VesselCertificateID);
                    table.ForeignKey(
                        name: "FK_VesselCertificate_Vessel_VesselID",
                        column: x => x.VesselID,
                        principalTable: "Vessel",
                        principalColumn: "VesselID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Certificate",
                columns: table => new
                {
                    SysUserId = table.Column<string>(maxLength: 50, nullable: true),
                    SysCompanyId = table.Column<int>(nullable: false),
                    CertificateID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: true),
                    Code = table.Column<string>(maxLength: 50, nullable: true),
                    IssuePlace = table.Column<string>(maxLength: 50, nullable: true),
                    IssueDate = table.Column<DateTime>(nullable: true),
                    ExpiryDate = table.Column<DateTime>(nullable: true),
                    NoticeDate = table.Column<DateTime>(nullable: true),
                    MaritimeBureau = table.Column<string>(maxLength: 20, nullable: true),
                    Department = table.Column<byte>(nullable: true),
                    Degree = table.Column<byte>(nullable: true),
                    VesselDegree = table.Column<byte>(nullable: true),
                    Nationality = table.Column<string>(maxLength: 20, nullable: true),
                    SailorID = table.Column<int>(nullable: false),
                    SailorName = table.Column<string>(maxLength: 10, nullable: true),
                    FileID = table.Column<int>(nullable: true),
                    CertificateTypeID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Certificate", x => x.CertificateID);
                    table.ForeignKey(
                        name: "FK_Certificate_Sailor_SailorID",
                        column: x => x.SailorID,
                        principalTable: "Sailor",
                        principalColumn: "SailorID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Contract",
                columns: table => new
                {
                    SysUserId = table.Column<string>(maxLength: 50, nullable: true),
                    SysCompanyId = table.Column<int>(nullable: false),
                    ContractID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ContractNo = table.Column<string>(maxLength: 50, nullable: true),
                    SigningDate = table.Column<DateTime>(nullable: true),
                    Post = table.Column<byte>(nullable: false),
                    Term = table.Column<int>(nullable: true),
                    AboardDate = table.Column<DateTime>(nullable: true),
                    AshoreDate = table.Column<DateTime>(nullable: true),
                    NoticeDate = table.Column<DateTime>(nullable: true),
                    Wage = table.Column<double>(nullable: true),
                    ShipWage = table.Column<double>(nullable: true),
                    HomeWage = table.Column<double>(nullable: true),
                    VacationWage = table.Column<double>(nullable: true),
                    ManagementFee = table.Column<double>(nullable: true),
                    AgencyFee = table.Column<double>(nullable: true),
                    ThirdPartyFee = table.Column<double>(nullable: true),
                    Remark = table.Column<string>(maxLength: 100, nullable: true),
                    SailorID = table.Column<int>(nullable: false),
                    SailorName = table.Column<string>(maxLength: 10, nullable: true),
                    ShipownerID = table.Column<int>(nullable: false),
                    ShipownerName = table.Column<string>(maxLength: 50, nullable: true),
                    VesselID = table.Column<int>(nullable: false),
                    VesselName = table.Column<string>(maxLength: 50, nullable: true),
                    WageInterval = table.Column<int>(nullable: false),
                    Complete = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contract", x => x.ContractID);
                    table.ForeignKey(
                        name: "FK_Contract_Sailor_SailorID",
                        column: x => x.SailorID,
                        principalTable: "Sailor",
                        principalColumn: "SailorID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Contract_Shipowner_ShipownerID",
                        column: x => x.ShipownerID,
                        principalTable: "Shipowner",
                        principalColumn: "ShipownerID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Contract_Vessel_VesselID",
                        column: x => x.VesselID,
                        principalTable: "Vessel",
                        principalColumn: "VesselID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Exam",
                columns: table => new
                {
                    SysUserId = table.Column<string>(maxLength: 50, nullable: true),
                    SysCompanyId = table.Column<int>(nullable: false),
                    ExamID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ApplyPost = table.Column<byte>(nullable: true),
                    ExamNo = table.Column<string>(maxLength: 50, nullable: true),
                    ExamDate = table.Column<DateTime>(nullable: true),
                    Expense = table.Column<double>(nullable: true),
                    ExpenseClaim = table.Column<double>(nullable: true),
                    CertificateNo = table.Column<string>(maxLength: 50, nullable: true),
                    IssueDate = table.Column<DateTime>(nullable: true),
                    Qualified = table.Column<bool>(nullable: false),
                    Remark = table.Column<string>(maxLength: 100, nullable: true),
                    SailorID = table.Column<int>(nullable: false),
                    SailorName = table.Column<string>(maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exam", x => x.ExamID);
                    table.ForeignKey(
                        name: "FK_Exam_Sailor_SailorID",
                        column: x => x.SailorID,
                        principalTable: "Sailor",
                        principalColumn: "SailorID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Experience",
                columns: table => new
                {
                    SysUserId = table.Column<string>(maxLength: 50, nullable: true),
                    SysCompanyId = table.Column<int>(nullable: false),
                    ExperienceID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    BeginTime = table.Column<DateTime>(nullable: true),
                    EndTime = table.Column<DateTime>(nullable: true),
                    Post = table.Column<byte>(nullable: true),
                    CompanyName = table.Column<string>(maxLength: 50, nullable: true),
                    SailorID = table.Column<int>(nullable: false),
                    SailorName = table.Column<string>(maxLength: 10, nullable: true),
                    VesselName = table.Column<string>(maxLength: 50, nullable: true),
                    IMO = table.Column<string>(maxLength: 20, nullable: true),
                    DurationMonth = table.Column<int>(nullable: true),
                    VesselType = table.Column<byte>(nullable: true),
                    Flag = table.Column<string>(maxLength: 50, nullable: true),
                    DeadWeightTon = table.Column<int>(nullable: true),
                    MainEngine = table.Column<string>(maxLength: 200, nullable: true),
                    Power = table.Column<string>(maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Experience", x => x.ExperienceID);
                    table.ForeignKey(
                        name: "FK_Experience_Sailor_SailorID",
                        column: x => x.SailorID,
                        principalTable: "Sailor",
                        principalColumn: "SailorID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Family",
                columns: table => new
                {
                    SysUserId = table.Column<string>(maxLength: 50, nullable: true),
                    SysCompanyId = table.Column<int>(nullable: false),
                    FamilyID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 20, nullable: false),
                    Relationship = table.Column<string>(maxLength: 10, nullable: true),
                    Beneficiary = table.Column<bool>(nullable: false),
                    Telephone = table.Column<string>(maxLength: 20, nullable: true),
                    Address = table.Column<string>(maxLength: 50, nullable: true),
                    Remark = table.Column<string>(maxLength: 50, nullable: true),
                    SailorID = table.Column<int>(nullable: false),
                    SailorName = table.Column<string>(maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Family", x => x.FamilyID);
                    table.ForeignKey(
                        name: "FK_Family_Sailor_SailorID",
                        column: x => x.SailorID,
                        principalTable: "Sailor",
                        principalColumn: "SailorID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Interview",
                columns: table => new
                {
                    SysUserId = table.Column<string>(maxLength: 50, nullable: true),
                    SysCompanyId = table.Column<int>(nullable: false),
                    InterviewID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Post = table.Column<byte>(nullable: false),
                    EnglishLevel = table.Column<byte>(nullable: true),
                    Listening = table.Column<byte>(nullable: true),
                    Speaking = table.Column<byte>(nullable: true),
                    Reading = table.Column<byte>(nullable: true),
                    Writing = table.Column<byte>(nullable: true),
                    Expertise = table.Column<byte>(nullable: true),
                    Qualification = table.Column<byte>(nullable: true),
                    EmergencyHandle = table.Column<byte>(nullable: true),
                    ServiceAwareness = table.Column<byte>(nullable: true),
                    Health = table.Column<byte>(nullable: true),
                    Management = table.Column<byte>(nullable: true),
                    SmsOperation = table.Column<byte>(nullable: true),
                    Other = table.Column<string>(maxLength: 100, nullable: true),
                    InterviewScore = table.Column<string>(maxLength: 10, nullable: false),
                    Conclusion = table.Column<byte>(nullable: false),
                    Comment = table.Column<string>(maxLength: 100, nullable: false),
                    Interviewer = table.Column<string>(maxLength: 10, nullable: false),
                    InterviewDate = table.Column<DateTime>(nullable: true),
                    InterviewPlace = table.Column<string>(maxLength: 10, nullable: true),
                    ProfessionalScore = table.Column<string>(maxLength: 10, nullable: true),
                    ComprehensiveScore = table.Column<string>(maxLength: 10, nullable: true),
                    ComprehensiveAssessment = table.Column<string>(maxLength: 10, nullable: true),
                    SailorRequirement = table.Column<string>(maxLength: 100, nullable: true),
                    SailorID = table.Column<int>(nullable: false),
                    SailorName = table.Column<string>(maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Interview", x => x.InterviewID);
                    table.ForeignKey(
                        name: "FK_Interview_Sailor_SailorID",
                        column: x => x.SailorID,
                        principalTable: "Sailor",
                        principalColumn: "SailorID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Title",
                columns: table => new
                {
                    SysUserId = table.Column<string>(maxLength: 50, nullable: true),
                    SysCompanyId = table.Column<int>(nullable: false),
                    TitleID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Approach = table.Column<string>(maxLength: 50, nullable: true),
                    BeginDate = table.Column<DateTime>(nullable: true),
                    EndDate = table.Column<DateTime>(nullable: true),
                    Work = table.Column<string>(maxLength: 50, nullable: true),
                    Major = table.Column<string>(maxLength: 50, nullable: true),
                    Category = table.Column<string>(maxLength: 50, nullable: true),
                    Post = table.Column<byte>(nullable: true),
                    Company = table.Column<string>(maxLength: 50, nullable: true),
                    EngageDate = table.Column<DateTime>(nullable: true),
                    Remark = table.Column<string>(maxLength: 100, nullable: true),
                    SailorID = table.Column<int>(nullable: false),
                    SailorName = table.Column<string>(maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Title", x => x.TitleID);
                    table.ForeignKey(
                        name: "FK_Title_Sailor_SailorID",
                        column: x => x.SailorID,
                        principalTable: "Sailor",
                        principalColumn: "SailorID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Traine",
                columns: table => new
                {
                    SysUserId = table.Column<string>(maxLength: 50, nullable: true),
                    SysCompanyId = table.Column<int>(nullable: false),
                    TraineeID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Expense = table.Column<double>(nullable: true),
                    ExpenseClaim = table.Column<double>(nullable: true),
                    CertificateNo = table.Column<string>(maxLength: 50, nullable: true),
                    Qualified = table.Column<bool>(nullable: false),
                    Remark = table.Column<string>(maxLength: 100, nullable: true),
                    TrainingClassID = table.Column<int>(nullable: false),
                    TrainingClassName = table.Column<string>(maxLength: 50, nullable: true),
                    SailorID = table.Column<int>(nullable: false),
                    SailorName = table.Column<string>(maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Traine", x => x.TraineeID);
                    table.ForeignKey(
                        name: "FK_Traine_Sailor_SailorID",
                        column: x => x.SailorID,
                        principalTable: "Sailor",
                        principalColumn: "SailorID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Traine_TrainingClass_TrainingClassID",
                        column: x => x.TrainingClassID,
                        principalTable: "TrainingClass",
                        principalColumn: "TrainingClassID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VesselCostPayment",
                columns: table => new
                {
                    VesselCostPaymentID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Payment = table.Column<double>(nullable: true),
                    USPayment = table.Column<double>(nullable: true),
                    Debt = table.Column<double>(nullable: true),
                    USDebt = table.Column<double>(nullable: true),
                    PaymentDate = table.Column<DateTime>(nullable: false),
                    VesselAccountID = table.Column<int>(nullable: false),
                    ReceiptFileID = table.Column<int>(nullable: true),
                    Remark = table.Column<string>(maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VesselCostPayment", x => x.VesselCostPaymentID);
                    table.ForeignKey(
                        name: "FK_VesselCostPayment_VesselAccount_VesselAccountID",
                        column: x => x.VesselAccountID,
                        principalTable: "VesselAccount",
                        principalColumn: "VesselAccountID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServiceRecord",
                columns: table => new
                {
                    SysUserId = table.Column<string>(maxLength: 50, nullable: true),
                    SysCompanyId = table.Column<int>(nullable: false),
                    ServiceRecordID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Post = table.Column<byte>(nullable: false),
                    SailZone = table.Column<string>(maxLength: 50, nullable: true),
                    AboardDate = table.Column<DateTime>(nullable: true),
                    AboardPlace = table.Column<string>(maxLength: 50, nullable: true),
                    AshoreDate = table.Column<DateTime>(nullable: true),
                    AshorePlace = table.Column<string>(maxLength: 50, nullable: true),
                    AshoreReason = table.Column<string>(maxLength: 50, nullable: true),
                    Remark = table.Column<string>(maxLength: 200, nullable: true),
                    Comment = table.Column<string>(maxLength: 200, nullable: true),
                    SailorID = table.Column<int>(nullable: false),
                    ShipownerID = table.Column<int>(nullable: false),
                    VesselID = table.Column<int>(nullable: false),
                    SailorName = table.Column<string>(maxLength: 10, nullable: true),
                    ShipownerName = table.Column<string>(maxLength: 50, nullable: true),
                    VesselName = table.Column<string>(maxLength: 50, nullable: true),
                    ContractID = table.Column<int>(nullable: true),
                    Complete = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceRecord", x => x.ServiceRecordID);
                    table.ForeignKey(
                        name: "FK_ServiceRecord_Contract_ContractID",
                        column: x => x.ContractID,
                        principalTable: "Contract",
                        principalColumn: "ContractID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ServiceRecord_Sailor_SailorID",
                        column: x => x.SailorID,
                        principalTable: "Sailor",
                        principalColumn: "SailorID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServiceRecord_Shipowner_ShipownerID",
                        column: x => x.ShipownerID,
                        principalTable: "Shipowner",
                        principalColumn: "ShipownerID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServiceRecord_Vessel_VesselID",
                        column: x => x.VesselID,
                        principalTable: "Vessel",
                        principalColumn: "VesselID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Wage",
                columns: table => new
                {
                    SysUserId = table.Column<string>(maxLength: 50, nullable: true),
                    SysCompanyId = table.Column<int>(nullable: false),
                    WageID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Year = table.Column<int>(nullable: false),
                    Month = table.Column<int>(nullable: false),
                    MonthlyDays = table.Column<int>(nullable: false),
                    BeginDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    WorkDays = table.Column<int>(nullable: false),
                    StandardWage = table.Column<double>(nullable: true),
                    ShouldWage = table.Column<double>(nullable: true),
                    ContractID = table.Column<int>(nullable: false),
                    SailorID = table.Column<int>(nullable: false),
                    SailorName = table.Column<string>(maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wage", x => x.WageID);
                    table.ForeignKey(
                        name: "FK_Wage_Contract_ContractID",
                        column: x => x.ContractID,
                        principalTable: "Contract",
                        principalColumn: "ContractID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Wage_Sailor_SailorID",
                        column: x => x.SailorID,
                        principalTable: "Sailor",
                        principalColumn: "SailorID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExamItem",
                columns: table => new
                {
                    ExamItemID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ItemName = table.Column<string>(maxLength: 50, nullable: true),
                    ExamDate = table.Column<DateTime>(nullable: true),
                    Score = table.Column<int>(nullable: false),
                    Qualified = table.Column<bool>(nullable: false),
                    ExamID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamItem", x => x.ExamItemID);
                    table.ForeignKey(
                        name: "FK_ExamItem_Exam_ExamID",
                        column: x => x.ExamID,
                        principalTable: "Exam",
                        principalColumn: "ExamID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Certificate_SailorID",
                table: "Certificate",
                column: "SailorID");

            migrationBuilder.CreateIndex(
                name: "IX_Contract_SailorID",
                table: "Contract",
                column: "SailorID");

            migrationBuilder.CreateIndex(
                name: "IX_Contract_ShipownerID",
                table: "Contract",
                column: "ShipownerID");

            migrationBuilder.CreateIndex(
                name: "IX_Contract_VesselID",
                table: "Contract",
                column: "VesselID");

            migrationBuilder.CreateIndex(
                name: "IX_Exam_SailorID",
                table: "Exam",
                column: "SailorID");

            migrationBuilder.CreateIndex(
                name: "IX_ExamItem_ExamID",
                table: "ExamItem",
                column: "ExamID");

            migrationBuilder.CreateIndex(
                name: "IX_Experience_SailorID",
                table: "Experience",
                column: "SailorID");

            migrationBuilder.CreateIndex(
                name: "IX_Family_SailorID",
                table: "Family",
                column: "SailorID");

            migrationBuilder.CreateIndex(
                name: "IX_Interview_SailorID",
                table: "Interview",
                column: "SailorID");

            migrationBuilder.CreateIndex(
                name: "IX_LaborSupplyPut_LaborSupplyID",
                table: "LaborSupplyPut",
                column: "LaborSupplyID");

            migrationBuilder.CreateIndex(
                name: "IX_LaborSupplyTake_LaborSupplyID",
                table: "LaborSupplyTake",
                column: "LaborSupplyID");

            migrationBuilder.CreateIndex(
                name: "IX_Sailor_VesselID",
                table: "Sailor",
                column: "VesselID");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceRecord_ContractID",
                table: "ServiceRecord",
                column: "ContractID");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceRecord_SailorID",
                table: "ServiceRecord",
                column: "SailorID");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceRecord_ShipownerID",
                table: "ServiceRecord",
                column: "ShipownerID");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceRecord_VesselID",
                table: "ServiceRecord",
                column: "VesselID");

            migrationBuilder.CreateIndex(
                name: "IX_Title_SailorID",
                table: "Title",
                column: "SailorID");

            migrationBuilder.CreateIndex(
                name: "IX_Traine_SailorID",
                table: "Traine",
                column: "SailorID");

            migrationBuilder.CreateIndex(
                name: "IX_Traine_TrainingClassID",
                table: "Traine",
                column: "TrainingClassID");

            migrationBuilder.CreateIndex(
                name: "IX_Vessel_ShipownerID",
                table: "Vessel",
                column: "ShipownerID");

            migrationBuilder.CreateIndex(
                name: "IX_VesselAccount_CompanyID",
                table: "VesselAccount",
                column: "CompanyID");

            migrationBuilder.CreateIndex(
                name: "IX_VesselAccount_VesselID",
                table: "VesselAccount",
                column: "VesselID");

            migrationBuilder.CreateIndex(
                name: "IX_VesselBalance_ShipownerID",
                table: "VesselBalance",
                column: "ShipownerID");

            migrationBuilder.CreateIndex(
                name: "IX_VesselCertificate_VesselID",
                table: "VesselCertificate",
                column: "VesselID");

            migrationBuilder.CreateIndex(
                name: "IX_VesselCostPayment_VesselAccountID",
                table: "VesselCostPayment",
                column: "VesselAccountID");

            migrationBuilder.CreateIndex(
                name: "IX_Wage_ContractID",
                table: "Wage",
                column: "ContractID");

            migrationBuilder.CreateIndex(
                name: "IX_Wage_SailorID",
                table: "Wage",
                column: "SailorID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Certificate");

            migrationBuilder.DropTable(
                name: "CertificateType");

            migrationBuilder.DropTable(
                name: "ExamItem");

            migrationBuilder.DropTable(
                name: "Experience");

            migrationBuilder.DropTable(
                name: "Family");

            migrationBuilder.DropTable(
                name: "Interview");

            migrationBuilder.DropTable(
                name: "LaborSupplyPut");

            migrationBuilder.DropTable(
                name: "LaborSupplyTake");

            migrationBuilder.DropTable(
                name: "Notice");

            migrationBuilder.DropTable(
                name: "ServiceRecord");

            migrationBuilder.DropTable(
                name: "SysCompany");

            migrationBuilder.DropTable(
                name: "Title");

            migrationBuilder.DropTable(
                name: "Traine");

            migrationBuilder.DropTable(
                name: "UploadFile");

            migrationBuilder.DropTable(
                name: "VesselBalance");

            migrationBuilder.DropTable(
                name: "VesselCertificate");

            migrationBuilder.DropTable(
                name: "VesselCostPayment");

            migrationBuilder.DropTable(
                name: "Wage");

            migrationBuilder.DropTable(
                name: "Exam");

            migrationBuilder.DropTable(
                name: "LaborSupply");

            migrationBuilder.DropTable(
                name: "TrainingClass");

            migrationBuilder.DropTable(
                name: "VesselAccount");

            migrationBuilder.DropTable(
                name: "Contract");

            migrationBuilder.DropTable(
                name: "Company");

            migrationBuilder.DropTable(
                name: "Sailor");

            migrationBuilder.DropTable(
                name: "Vessel");

            migrationBuilder.DropTable(
                name: "Shipowner");
        }
    }
}

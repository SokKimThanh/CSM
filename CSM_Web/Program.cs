using CSM_Common.Common;
using CSM_Data_Access.Cache.Sys;
using CSM_Data_Access.Utility;
using CSM_Web.Thread;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

// Connection
CConfig.CSM_Cinema_DB_Conn_String = builder.Configuration.GetConnectionString("CSM_Cinema_DB_Conn_String");

//Định dạng ngày tháng
CConfig.Short_Day_Format_String = builder.Configuration.GetValue<string>("Short_Day_Format_String");

//Cấu hình sản phẩm
CConfig.Product_Name = builder.Configuration.GetValue<string>("Product_Name");
CConfig.Product_Version = builder.Configuration.GetValue<string>("Product_Version");
CConfig.Product_Auth = builder.Configuration.GetValue<string>("Company_Name");
CConfig.Created = CUtility.Convert_To_DateTime(builder.Configuration.GetValue<string>("Created"));
CConfig.Created_By = builder.Configuration.GetValue<string>("Created_By");

//Time out request
CConfig.Time_Out = builder.Configuration.GetValue<int>("Time_Out");

//Cấu hình gửi mail
CConfig.Smtp_Host = builder.Configuration.GetValue<string>("Smtp_Host");
CConfig.Smtp_UseDefaultCredentials = builder.Configuration.GetValue<bool>("Smtp_UseDefaultCredentials");
CConfig.Smtp_Port = builder.Configuration.GetValue<int>("Smtp_Port");
CConfig.Smtp_User = builder.Configuration.GetValue<string>("Smtp_User");
CConfig.Smtp_Pass = builder.Configuration.GetValue<string>("Smtp_Pass");
CConfig.Smtp_EnableSsl = builder.Configuration.GetValue<bool>("Smtp_EnableSsl");
CConfig.Email_From = builder.Configuration.GetValue<string>("Email_From");

//Các luồng chạy ngầm
builder.Services.AddHostedService<CCache_Timer_Service>();
builder.Services.AddHostedService<CTheard_1_Service>();

//Các biến common cần set lại mặc định nếu cần
CCache_Common.Is_Load_Cache_Completed = false;

//Tại đây load cache sys
CCache_Sys_Thanh_Vien.Load_Thanh_Vien();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseStaticFiles();
app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();

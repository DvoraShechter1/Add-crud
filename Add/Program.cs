using Add;
using System;
using System.Collections;
using System.CommandLine;
using System.Diagnostics.SymbolStore;
using System.Diagnostics.Tracing;
using static Add.Function;

//יצירת פעולה המופעלת ע"י שורת הפקודה
var crudCommand = new Command("crud", "create, read, update, delete");

//הגדרת המשתנים שהפעולה מקבלת
var ar1 = new Argument<string>("source class");
var ar2 = new Argument<string>("destination class");
//הוספת המשתנים לפעולה
crudCommand.AddArgument(ar1);
crudCommand.AddArgument(ar2);

//הגדרת אופציות שהפעולה יכולה לקבל
var option1 = new Option<string>("--dal", "Name of the dal project");
var option2 = new Option<string>("--bll", "Name of the bll project");
var option3 = new Option<string>("--dto", "Name of the dto project");
var option4 = new Option<string>("--web", "Name of the web project");
//הוספת האופציות לפעולה
crudCommand.AddOption(option1);
crudCommand.AddOption(option2);
crudCommand.AddOption(option3);
crudCommand.AddOption(option4);


//יצירת האירוע
crudCommand.SetHandler((sClass, dClass, dal, bll, dto, web) =>
{

    //הצבת הנתונים במילון
    n.Add("sClass", sClass);
    n.Add("dClass", dClass);

    if (dal != null)
        n["Repository_Dal"]=dal;
    if (bll != null)
        n["Service_BLL"]=bll;
    if (dto != null)
        n["Common_DTO"]=dto;
    if (web != null)
        n["WebApi"]=web;


    //======================= Dal בניית הקבצים ב ===================================
    //מעודכן dal בדיקה ששם פרויקט ה 
    if (!Directory.Exists(n["Repository_Dal"]))
        throw new Exception("The name of the DAL project does not match\r\nYou may use the --dal option");
    //יצירת תיקייה לממשקים
    if (!Directory.Exists(n["Repository_Dal"]+"\\Interfaces"))
        Directory.CreateDirectory(n["Repository_Dal"]+"\\Interfaces");
    //יצירת הקובץ
    File.WriteAllText(n["Repository_Dal"]+"\\Interfaces\\"+n["sClass"]+".cs", interfaceDal());
    //יצירת תיקייה למימושים
    if (!Directory.Exists(n["Repository_Dal"]+"\\Repositories"))
        Directory.CreateDirectory(n["Repository_Dal"]+"\\Repositories");
    //יצירת הקובץ
    File.WriteAllText(n["Repository_Dal"]+"\\Repositories\\"+n["sClass"]+".cs", functionDal());

    //======================= bll בניית הקבצים ב ===================================
    //מעודכן bll בדיקה ששם פרויקט ה 
    if (!Directory.Exists(n["Service_BLL"]))
        throw new Exception("The name of the BLL project does not match\r\nYou may use the --bll option");
    //יצירת תיקייה לממשקים
    if (!Directory.Exists(n["Service_BLL"]+"\\Interfaces"))
        Directory.CreateDirectory(n["Service_BLL"]+"\\Interfaces");
    //יצירת הקובץ
    File.WriteAllText(n["Service_BLL"]+"\\Interfaces\\"+n["sClass"]+".cs", interfaceBll());
    //יצירת תיקייה למימושים
    if (!Directory.Exists(n["Service_BLL"]+"\\Services"))
        Directory.CreateDirectory(n["Service_BLL"]+"\\Services");
    //יצירת הקובץ
    File.WriteAllText(n["Service_BLL"]+"\\Services\\"+n["sClass"]+".cs", functionBll());


    //======================= web בניית הקןבץ ב ===================================
    // מעודכן ויש בו תיקייה מתאימה web בדיקה ששם פרויקט ה 
    if (!Directory.Exists(n["WebApi"]+"\\Controllers"))
        throw new Exception("The name of the web project does not match or is not of the appropriate type\r\nYou may use the --web option");
    //יצירת הקובץ
    File.WriteAllText(n["WebApi"]+"\\Controllers\\"+n["sClass"]+"Controller.cs", controller());

    Console.WriteLine("The files have been created." +
        "\r\nYou need to add the following lines to the dependency injection files:\n" +
        "services.AddScoped<I" +sClass +"Repository, " +sClass+"Repository>();\r\n" +
        " services.AddScoped<I" +sClass+ "Service, " + sClass+  "Service>();\r\n");

}, ar1,ar2, option1, option2, option3, option4);


var rootCommand = new RootCommand("A command to add actions to a 3-tier project");
rootCommand.AddCommand(crudCommand);
rootCommand.InvokeAsync(args);
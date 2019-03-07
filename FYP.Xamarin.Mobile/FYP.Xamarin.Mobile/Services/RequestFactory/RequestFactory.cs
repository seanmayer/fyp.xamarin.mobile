using FYP.Xamarin.Mobile.IServices;
using FYP.Xamarin.Mobile.Services.RequestFactory;
using FYP.Xamarin.Mobile.Services.Requests;
using System;
using System.Net;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace FYP.Xamarin.Mobile.Services.RequestFactory
{ 

public class RequestFactory
{

    private static RequestFactory Instance = new RequestFactory();

    private readonly int SERVER_PORT = 8080;
    private readonly string SERVER_IP  = "192.168.0.130";
    private readonly string _PROJECT_PACKAGE = "FYP.SCPSAA.Web.Services/";
    private readonly string _SERVICE_PACKAGE = "webresources/";

    public static RequestFactory GetSingleton()
    {
        return Instance;
    }

    public string SERVER_ADDRESS
    {
        get => "http://" + SERVER_IP + ":" + SERVER_PORT + "/";
    }
    public string PROJECT_PACKAGE
    {
        get => SERVER_ADDRESS + _PROJECT_PACKAGE;
    }
    public string SERVICE_PACKAGE
    {
        get => PROJECT_PACKAGE + _SERVICE_PACKAGE;
    }
    public string CREATE_ATHLETE
    {
        get => SERVICE_PACKAGE + new AthleteRequest().MakeCreateRequest();
    }

    public string FIND_ATHLETE
    {
        get => SERVICE_PACKAGE + new AthleteRequest().MakeGetRequest();
    }

    public string LIST_ATHLETE
    {
        get => SERVICE_PACKAGE + new AthleteRequest().MakeListRequest();
    }
    public string CREATE_CREDENTIALS
    {
        get => SERVICE_PACKAGE + new CredentialsRequest().MakeCreateRequest();
    }
    public string LIST_CREDENTIALS
    {
        get => SERVICE_PACKAGE + new CredentialsRequest().MakeListRequest();
    }
    public string LIST_ACTIVITIES
    {
        get => SERVICE_PACKAGE + new ActivityRequest().MakeListRequest();
    }


}



}

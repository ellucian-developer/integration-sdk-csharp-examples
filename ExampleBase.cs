/*
 * ******************************************************************************
 *   Copyright  2022 Ellucian Company L.P. and its affiliates.
 * ******************************************************************************
 */

using Ellucian.Ethos.Integration.Client;
using Ellucian.Ethos.Integration.Client.Errors;
using Ellucian.Ethos.Integration.Client.Proxy;
using Ellucian.Ethos.Integration.Config;
using Ellucian.Ethos.Integration.StronglyTyped.Colleague.Example;

namespace Ellucian.Examples;
public class ExampleBase
{
    internal protected static string SAMPLE_API_KEY = string.Empty;
    internal protected static string RECORD_GUID = string.Empty;
    internal protected static EthosProxyClient proxyClient = default!;
    internal protected static EthosFilterQueryClient filterClient = default!;
    internal protected static EthosErrorsClient errorsClient = default!;
    internal protected static EthosConfigurationClient configurationClient = default!;
    private static EthosClientBuilder ethosClientBuilder = default!;

    public static async Task Run( string apiKey )
    {
        SAMPLE_API_KEY = apiKey;
        await RunEEDMExamplesAsync();
        await RunBannerBPAPIExamplesAsync();
        await RunColleagueBPAPIExamplesAsync();
        await RunProxyClientExampleAsync();
        await RunMessageClientExampleAsync();
        await RunFilterQueryClientExampleAsync();
        await RunGetAccessTokenExampleAsync();
        await RunEthosErrorsClientExampleAsync();
        await RunEthosConfigurationClientExampleAsync();
        await RunEthosChangeNotificationSubscriberExampleAsync();
        await RunEthosChangeNotificationServiceExampleAsync();
    }

    #region Examples
    private static async Task RunEEDMExamplesAsync()
    {
        Console.WriteLine( "---------------------------------- RunEEDMExamplesAsync ----------------------------------" );
        await EEDMExample.Run();
    }

    private static async Task RunBannerBPAPIExamplesAsync()
    {
        Console.WriteLine( "---------------------------------- RunBannerBPAPIExamplesAsync ----------------------------------" );
        await BannerBPAPIExample.Run();
    }

    private static async Task RunColleagueBPAPIExamplesAsync()
    {
        Console.WriteLine( "---------------------------------- RunColleagueBPAPIExamplesAsync ----------------------------------" );
        await ColleagueBPAPIExample.Run();
    }

    private static async Task RunProxyClientExampleAsync()
    {
        Console.WriteLine( "---------------------------------- RunProxyClientExampleAsync ----------------------------------" );
        await EthosProxyClientExample.Run();
    }

    private static async Task RunMessageClientExampleAsync()
    {
        Console.WriteLine( "---------------------------------- RunMessageClientExampleAsync ----------------------------------" );
        await EthosMessagesClientExample.Run();
    }

    private static async Task RunFilterQueryClientExampleAsync()
    {
        Console.WriteLine( "---------------------------------- RunFilterQueryClientExampleAsync ----------------------------------" );

        await EthosFilterQueryClientExample.Run();
    }

    private static async Task RunGetAccessTokenExampleAsync()
    {
        Console.WriteLine( "---------------------------------- RunGetAccessTokenExampleAsync ----------------------------------" );

        await GetAccessTokenExample.Run();
    }

    private static async Task RunEthosErrorsClientExampleAsync()
    {
        Console.WriteLine( "---------------------------------- RunEthosErrorsClientExampleAsync ----------------------------------" );

        await EthosErrorsClientExample.Run();
    }

    private static async Task RunEthosConfigurationClientExampleAsync()
    {
        Console.WriteLine( "---------------------------------- RunEthosConfigurationClientExampleAsync ----------------------------------" );

        await EthosConfigurationClientExample.Run();
    }

    private static async Task RunEthosChangeNotificationSubscriberExampleAsync()
    {
        Console.WriteLine( "---------------------------------- RunEthosChangeNotificationSubscriberExampleAsync ----------------------------------" );

        await EthosChangeNotificationSubscriberExample.Run();
    }

    private static async Task RunEthosChangeNotificationServiceExampleAsync()
    {
        Console.WriteLine( "---------------------------------- RunEthosChangeNotificationServiceExampleAsync ----------------------------------" );
        await EthosChangeNotificationServiceExample.Run();
    }

    #endregion

    #region Helper Methods

    internal static EthosClientBuilder GetEthosClientBuilder()
    {
        return ethosClientBuilder = ethosClientBuilder ?? new EthosClientBuilder( SAMPLE_API_KEY ).WithConnectionTimeout( 240 );
    }

    internal static void BuildEthosProxyClient()
    {
        proxyClient = proxyClient ?? GetEthosClientBuilder().BuildEthosProxyClient();
    }


    internal static void BuildEthosFilterClient()
    {
        filterClient = filterClient ?? GetEthosClientBuilder().WithConnectionTimeout( 240 ).BuildEthosFilterQueryClient();
    }
    internal static void BuildEthosErrorsClient()
    {
        errorsClient = errorsClient ?? GetEthosClientBuilder().WithConnectionTimeout( 240 ).BuildEthosErrorsClient();
    }

    internal static void BuildConfigurationClient()
    {
        configurationClient = configurationClient ?? GetEthosClientBuilder().WithConnectionTimeout( 240 ).BuildEthosConfigurationClient();
    }

    internal static int GetRandomNumber( int totalNumRecords )
    {
        Random random = new();
        int num = random.Next( 0, totalNumRecords - 1 );
        return num!;
    }

    #endregion
}


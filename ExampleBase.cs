/*
 * ******************************************************************************
 *   Copyright  2022 Ellucian Company L.P. and its affiliates.
 * ******************************************************************************
 */

using Ellucian.Ethos.Integration.Client;
using Ellucian.Ethos.Integration.Client.Errors;
using Ellucian.Ethos.Integration.Client.Proxy;
using Ellucian.Ethos.Integration.Config;

namespace Ellucian.Examples;
public class ExampleBase
{
    internal static string SAMPLE_API_KEY = string.Empty;
    internal static string RECORD_GUID = string.Empty;
    internal static EthosProxyClient proxyClient = default!;
    internal static EthosFilterQueryClient filterClient = default!;
    internal static EthosErrorsClient errorsClient = default!;
    internal static EthosConfigurationClient configurationClient = default!;
    private static EthosClientBuilder ethosClientBuilder = default!;

    public static async Task Run( string apiKey )
    {
        SAMPLE_API_KEY = apiKey;
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

    private static async Task RunProxyClientExampleAsync()
    {
        Console.WriteLine( "---------------------------------- RunProxyClientExampleAsync ----------------------------------\n" );
        await EthosProxyClientExample.Run();
    }

    private static async Task RunMessageClientExampleAsync()
    {
        Console.WriteLine( "---------------------------------- RunMessageClientExampleAsync ----------------------------------\n" );
        await EthosMessagesClientExample.Run();
    }

    private static async Task RunFilterQueryClientExampleAsync()
    {
        Console.WriteLine( "---------------------------------- RunFilterQueryClientExampleAsync ----------------------------------\n" );
        await EthosFilterQueryClientExample.Run();
    }

    private static async Task RunGetAccessTokenExampleAsync()
    {
        Console.WriteLine( "---------------------------------- RunGetAccessTokenExampleAsync ----------------------------------\n" );
        await GetAccessTokenExample.Run();
    }

    private static async Task RunEthosErrorsClientExampleAsync()
    {
        Console.WriteLine( "---------------------------------- RunEthosErrorsClientExampleAsync ----------------------------------\n" );
        await EthosErrorsClientExample.Run();
    }

    private static async Task RunEthosConfigurationClientExampleAsync()
    {
        Console.WriteLine( "---------------------------------- RunEthosConfigurationClientExampleAsync ----------------------------------\n" );
        await EthosConfigurationClientExample.Run();
    }

    private static async Task RunEthosChangeNotificationSubscriberExampleAsync()
    {
        Console.WriteLine( "---------------------------------- RunEthosChangeNotificationSubscriberExampleAsync ----------------------------------\n" );
        await EthosChangeNotificationSubscriberExample.Run();
    }

    private static async Task RunEthosChangeNotificationServiceExampleAsync()
    {
        Console.WriteLine( "---------------------------------- RunEthosChangeNotificationServiceExampleAsync ----------------------------------\n" );
        await EthosChangeNotificationServiceExample.Run();
    }

    #endregion

    #region Helper Methods

    internal static EthosClientBuilder GetEthosClientBuilder()
    {
        return ethosClientBuilder ??= new EthosClientBuilder( SAMPLE_API_KEY ).WithConnectionTimeout( 240 );
    }

    internal static void BuildEthosProxyClient()
    {
        proxyClient ??= GetEthosClientBuilder().BuildEthosProxyClient();
    }

    internal static void BuildEthosFilterClient()
    {
        filterClient ??= GetEthosClientBuilder().WithConnectionTimeout( 240 ).BuildEthosFilterQueryClient();
    }
    internal static void BuildEthosErrorsClient()
    {
        errorsClient ??= GetEthosClientBuilder().WithConnectionTimeout( 240 ).BuildEthosErrorsClient();
    }

    internal static void BuildConfigurationClient()
    {
        configurationClient ??= GetEthosClientBuilder().WithConnectionTimeout( 240 ).BuildEthosConfigurationClient();
    }

    internal static int GetRandomNumber( int totalNumRecords )
    {
        Random random = new();
        int num = random.Next( 0, totalNumRecords - 1 );
        return num!;
    }

    #endregion
}


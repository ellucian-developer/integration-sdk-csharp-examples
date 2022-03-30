/*
 * ******************************************************************************
 *   Copyright  2022 Ellucian Company L.P. and its affiliates.
 * ******************************************************************************
 */

using Ellucian.Ethos.Integration.Client;
using Ellucian.Ethos.Integration.Client.Proxy;
using Ellucian.Ethos.Integration.StronglyTyped.Colleague.Example;

namespace Ellucian.Examples;
public class ExampleBase
{
    internal protected static string SAMPLE_API_KEY = string.Empty;
    internal protected static string RECORD_GUID = string.Empty;
    internal protected static EthosProxyClient client = default!;
    internal protected static EthosFilterQueryClient filterClient = default!;

    public static async Task Run(string apiKey)
    {
        SAMPLE_API_KEY = apiKey;
        await RunEEDMExamplesAsync();
        await RunBannerBPAPIExamplesAsync();
        await RunColleagueBPAPIExamplesAsync();
    }

    private static async Task RunEEDMExamplesAsync()
    {
        Console.WriteLine( "---------------------------------- EEDM EXAMPLES ----------------------------------" );
        await EEDMExample.RunEedmExamples();
        Console.WriteLine( "\n\n-------------------------------------------------------------------------------\n\n" );
    }

    private static async Task RunBannerBPAPIExamplesAsync()
    {
        Console.WriteLine( "---------------------------------- BANNER BPAPI EXAMPLES ----------------------------------" );
        await BannerBPAPIExample.RunBannerBpApiExamples();
        Console.WriteLine( "\n\n---------------------------------------------------------------------------------------\n\n" );
    }

    private static async Task RunColleagueBPAPIExamplesAsync()
    {
        Console.WriteLine( "---------------------------------- COLLEAGUE BPAPI EXAMPLES ----------------------------------" );
        await ColleagueBPAPIExample.RunColleagueBpApiExamples();
        Console.WriteLine( "\n\n------------------------------------------------------------------------------------------\n\n" );
    }
    internal static EthosProxyClient GetEthosProxyClient()
    {
        EthosClientBuilder ethosClientBUilder = new EthosClientBuilder( SAMPLE_API_KEY ).WithConnectionTimeout( 240 );
        return ethosClientBUilder.BuildEthosProxyClient();
    }

    internal static EthosFilterQueryClient GetEthosFilterClient()
    {
        EthosClientBuilder ethosClientBUilder = new EthosClientBuilder( SAMPLE_API_KEY ).WithConnectionTimeout( 240 );
        return ethosClientBUilder.BuildEthosFilterQueryClient();
    }

    internal static int GetRandomNumber( int totalNumRecords )
    {
        Random random = new();
        int num = random.Next( 0, totalNumRecords - 1 );
        return num!;
    }
}


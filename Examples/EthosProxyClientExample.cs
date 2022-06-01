/*
 * ******************************************************************************
 *   Copyright  2022 Ellucian Company L.P. and its affiliates.
 * ******************************************************************************
 */

using Ellucian.Ethos.Integration.Client;
using Ellucian.Ethos.Integration.Client.Proxy;
using Ellucian.Generated.BpApi.TermCodesV100GetRequest;
using Ellucian.Generated.Eedm;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ellucian.Examples;

/// <summary>
/// Methods to illustrate how to work with the Ellucian Ethos Integration C# SDK. These examples are the
/// most up to date place to see how to perform operations, and all operations should work. We use these to
/// verify this code too!
/// </summary>
public class EthosProxyClientExample : ExampleBase
{
    /// <summary>
    /// An example of how to use <see cref="EthosProxyClient"/>.
    /// <para>Example: </para>
    /// <code>var proxyClient = ethosClientFactory.GetEthosProxyClient( SAMPLE_API_KEY );</code>
    /// To call from command line pass parameters as follows, ProxyClientExample.exe &lt;SAMPLE_API_KEY&gt; RECORD_GUID
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    public static async Task Run()
    {
        BuildEthosProxyClient();
        await DoGetResourceByIdExample();
        await DoGetResourceAsStringByIdExample();
        await DoGetResourceAsJsonByIdExample();
        await DoGetResourcePageSizeExample();
        await DoGetResourceMaxPageSizeExample();
        await DoGetResourceExample();
        await DoGetResourceAsstringExample();
        await DoGetResourceAsJsonExample();
        await DoGetAllPagesExample();
        await DoGetAllPagesAsstringsExample();
        await DoGetAllPagesAsJsonsExample();
        await DoGetAllPagesFromOffsetExample();
        await DoGetAllPagesFromOffsetAsStringsExample();
        await DoGetAllPagesFromOffsetAsJsonsExample();
        await DoGetPagesExample();
        await DoGetPagesAsstringsExample();
        await DoGetPagesAsJsonsExample();
        await DoGetPagesFromOffsetExample();
        await DoGetPagesFromOffsetAsstringsExample();
        await DoGetPagesFromOffsetAsJsonsExample();
        await DoGetRowsExample();
        await DoGetRowsAsstringsExample();
        await DoGetRowsAsJsonExample();
        await DoGetRowsFromOffsetExample();
        await DoGetRowsFromOffsetAsstringsExample();
        await DoGetRowsFromOffsetAsJsonsExample();
        await DoSimplePersonsIterationExample();
        await DoCrudExample();

        await DoGetStudentCohortAsync();
        await DoGetRowsAsJsonStronglyTypedExample();
        await DoFullCrudWithStronglyTypedAsync();
        await DoGetTermCodesAsync();
    }

    #region All examples

    /// <summary>
    /// DoGetResourceByIdExample
    /// </summary>
    /// <returns></returns>
    private static async Task DoGetResourceByIdExample()
    {
        string resource = "student-cohorts";
        string id = RECORD_GUID;
        try
        {

            if ( !string.IsNullOrWhiteSpace( id ) )
            {
                EthosResponse? ethosResponse = await proxyClient.GetByIdAsync( resource, id );
                Console.WriteLine( "******* Get resource by ID example. *******" );
                Console.WriteLine( $"RESOURCE: { resource }" );
                Console.WriteLine( $"RESOURCE ID: { id }" );
                Console.WriteLine( $"RESPONSE: { ethosResponse.Content }" );
            }
        }
        catch ( Exception e )
        {
            Console.WriteLine( e.Message );
        }
    }

    /// <summary>
    /// DoGetResourceAsStringByIdExample
    /// </summary>
    /// <returns></returns>
    private static async Task DoGetResourceAsStringByIdExample()
    {
        string resource = "student-cohorts";
        string id = RECORD_GUID;

        try
        {

            if ( !string.IsNullOrWhiteSpace( id ) )
            {
                string ethosResponse = await proxyClient.GetAsStringByIdAsync( resource, id, "application/vnd.hedtech.integration.v7.2.0+json" );
                Console.WriteLine( "******* Get resource by ID example. *******" );
                Console.WriteLine( $"RESOURCE: { resource }" );
                Console.WriteLine( $"RESOURCE ID: { id }" );
                Console.WriteLine( $"RESPONSE: { ethosResponse }" );
            }
        }
        catch ( Exception e )
        {
            Console.WriteLine( e.Message );
        }
    }

    /// <summary>
    /// DoGetResourceAsJsonByIdExample
    /// </summary>
    /// <returns></returns>
    private static async Task DoGetResourceAsJsonByIdExample()
    {
        string resource = "student-cohorts";
        string id = RECORD_GUID;
        try
        {
            if ( !string.IsNullOrWhiteSpace( id ) )
            {
                var jsonNode = await proxyClient.GetAsJObjectByIdAsync( resource, id );
                Console.WriteLine( "******* Get resource by ID example. *******" );
                Console.WriteLine( $"RESOURCE: { resource }" );
                Console.WriteLine( $"RESOURCE ID: { id }" );
                Console.WriteLine( $"RESPONSE: { jsonNode}" );
            }
        }
        catch ( Exception e )
        {
            Console.WriteLine( e.Message );
        }
    }

    /// <summary>
    /// DoGetResourcePageSizeExample
    /// </summary>
    /// <returns></returns>
    private static async Task DoGetResourcePageSizeExample()
    {
        string resource = "student-cohorts";
        try
        {
            int pageSize = await proxyClient.GetPageSizeAsync( resource );
            Console.WriteLine( "******* Get resource page size example. *******" );
            Console.WriteLine( $"RESOURCE: {resource}" );
            Console.WriteLine( $"Page Size: {pageSize}" );
        }
        catch ( Exception e )
        {
            Console.WriteLine( e.Message );
        }
    }

    /// <summary>
    /// DoGetResourceMaxPageSizeExample
    /// </summary>
    /// <returns></returns>
    private static async Task DoGetResourceMaxPageSizeExample()
    {
        string resource = "student-cohorts";
        try
        {
            int pageSize = await proxyClient.GetMaxPageSizeAsync( resource );
            Console.WriteLine( "******* Get resource max page size example. *******" );
            Console.WriteLine( $"RESOURCE: {resource}" );
            Console.WriteLine( $"MAX PAGE SIZE: {pageSize}" );
        }
        catch ( Exception e )
        {
            Console.WriteLine( e.Message );
        }
    }

    /// <summary>
    /// DoGetResourceExample
    /// </summary>
    /// <returns></returns>
    private static async Task DoGetResourceExample()
    {
        try
        {
            string resourceName = "student-cohorts";
            EthosResponse? ethosResponse = await proxyClient.GetAsync( resourceName, string.Empty );
            string totalCountHdr = ethosResponse.GetHeader( EthosProxyClient.HDR_X_TOTAL_COUNT );
            Console.WriteLine( "******* Get data for resource example, no paging. *******" );
            Console.WriteLine( $"Get data for resource: {resourceName}" );
            PrintHeaders( ethosResponse );
            Console.WriteLine( "getResource() TOTAL COUNT: " + totalCountHdr );
            Console.WriteLine( "getResource() RESPONSE: " + ethosResponse );
        }
        catch ( Exception e )
        {
            Console.WriteLine( e.Message );
        }
    }

    /// <summary>
    /// DoGetResourceAsstringExample
    /// </summary>
    /// <returns></returns>
    private static async Task DoGetResourceAsstringExample()
    {
        try
        {
            string resourceName = "student-cohorts";
            string response = await proxyClient.GetAsStringAsync( resourceName, string.Empty );
            string jsonNode = JsonConvert.SerializeObject( response );
            Console.WriteLine( "******* Get data for resource as string example, no paging. *******" );
            Console.WriteLine( $"Get data for resource: { resourceName }" );
            Console.WriteLine( "getResource() PAGE SIZE: " + jsonNode.Length );
            Console.WriteLine( "getResource() RESPONSE: " + response );
        }
        catch ( Exception e )
        {
            Console.WriteLine( e.Message );
        }
    }

    /// <summary>
    /// DoGetResourceAsJsonExample
    /// </summary>
    /// <returns></returns>
    private static async Task DoGetResourceAsJsonExample()
    {
        try
        {
            string resourceName = "student-cohorts";
            var jsonNode = await proxyClient.GetAsJArrayAsync( resourceName, "" );
            Console.WriteLine( "******* Get data for resource as Json example, no paging. *******" );
            Console.WriteLine( $"Get data for resource: { resourceName }" );
            Console.WriteLine( "getResource() PAGE SIZE: " + jsonNode.Count );
            Console.WriteLine( "getResource() RESPONSE: " + jsonNode.ToString() );
        }
        catch ( Exception e )
        {
            Console.WriteLine( e.Message );
        }
    }

    /// <summary>
    /// DoGetAllPagesExample
    /// </summary>
    /// <returns></returns>
    private static async Task DoGetAllPagesExample()
    {
        try
        {
            string resourceName = "student-cohorts";
            var ethosResponseList = await proxyClient.GetAllPagesAsync( resourceName, "", 15 );
            Console.WriteLine( "******* Get all pages with page size example. *******" );
            Console.WriteLine( $"Get data for resource: { resourceName }" );
            int counter = ethosResponseList.Count();
            for ( int i = 0; i < counter; i++ )
            {
                var content = ethosResponseList.ElementAt( i ).Content;
                Console.WriteLine( $"PAGE { i + 1 } : { content }" );
                Console.WriteLine( $"PAGE { i + 1 } SIZE: { content.Length }" );
            }
        }
        catch ( Exception e )
        {
            Console.WriteLine( e.Message );
        }
    }

    /// <summary>
    /// DoGetAllPagesAsstringsExample
    /// </summary>
    /// <returns></returns>
    private static async Task DoGetAllPagesAsstringsExample()
    {
        try
        {
            string resourceName = "student-cohorts";
            //ObjectMapper objectMapper = new ObjectMapper();
            var stringList = await proxyClient.GetAllPagesAsStringsAsync( resourceName, "", 15 );
            Console.WriteLine( "******* Get all pages as strings with page size example. *******" );
            Console.WriteLine( $"Get data for resource: { resourceName }" );
            int counter = stringList.Count();
            for ( int i = 0; i < counter; i++ )
            {
                string jsonNode = stringList.ElementAt( i );
                Console.WriteLine( $"PAGE { i + 1 }: { stringList.ElementAt( i ) }" );
                Console.WriteLine( $"PAGE { i + 1 }: SIZE: { jsonNode.Length}" );
            }
        }
        catch ( Exception e )
        {
            Console.WriteLine( e.Message );
        }
    }

    /// <summary>
    /// DoGetAllPagesAsJsonsExample
    /// </summary>
    /// <returns></returns>
    private static async Task DoGetAllPagesAsJsonsExample()
    {
        try
        {
            string resourceName = "student-cohorts";
            var jsonNodeList = await proxyClient.GetAllPagesAsJArraysAsync( resourceName, "", 15 );
            Console.WriteLine( "******* Get all pages as Jsons with page size example. *******" );
            Console.WriteLine( $"Get data for resource: { resourceName }" );
            int counter = jsonNodeList.Count();

            for ( int i = 0; i < counter; i++ )
            {
                var jsonNode = jsonNodeList.ElementAt( i );
                Console.WriteLine( $"PAGE { i + 1 }: { jsonNode.ToString() }" );
                Console.WriteLine( $"PAGE { i + 1 } SIZE: { jsonNode.Count }" );
            }
        }
        catch ( Exception e )
        {
            Console.WriteLine( e.Message );
        }
    }

    /// <summary>
    /// DoGetAllPagesFromOffsetExample
    /// </summary>
    /// <returns></returns>
    private static async Task DoGetAllPagesFromOffsetExample()
    {
        try
        {
            string resourceName = "student-cohorts";
            int totalCount = await proxyClient.GetTotalCountAsync( resourceName );
            // Calculate the offset to be 95% of the totalCount to avoid paging through potentially tons of pages.
            int offset = ( int ) ( totalCount * 0.95 );
            var ethosResponseList = await proxyClient.GetAllPagesFromOffsetAsync( resourceName, "", offset );
            Console.WriteLine( "******* Get all pages from offset example. *******" );
            Console.WriteLine( $"Get data for resource: { resourceName }" );
            Console.WriteLine( $"Calculated offset of { offset } which is 95 percent of a total count of { totalCount } to avoid paging through potentially lots of pages." );
            Console.WriteLine( "To run with more paging, manually set the offset to a lower value, or reduce the percentage of the total count." );
            int counter = ethosResponseList.Count();
            for ( int i = 0; i < counter; i++ )
            {
                var jsonNode = ethosResponseList.ElementAt( i ).Content;
                Console.WriteLine( $"PAGE { i + 1 }: { ethosResponseList.ElementAt( i ).Content}" );
                Console.WriteLine( $"PAGE { i + 1 } SIZE: { jsonNode.Length }" );
                Console.WriteLine( $"OFFSET: { offset }" );
            }
        }
        catch ( Exception e )
        {
            Console.WriteLine( e.Message );
        }
    }

    /// <summary>
    /// DoGetAllPagesFromOffsetAsstringsExample
    /// </summary>
    /// <returns></returns>
    private static async Task DoGetAllPagesFromOffsetAsStringsExample()
    {
        try
        {
            string resourceName = "student-cohorts";
            int totalCount = await proxyClient.GetTotalCountAsync( resourceName );
            // Calculate the offset to be 95% of the totalCount to avoid paging through potentially tons of pages.
            int offset = ( int ) ( totalCount * 0.95 );
            var stringList = await proxyClient.GetAllPagesFromOffsetAsStringsAsync( resourceName, "", offset, 0 );
            Console.WriteLine( "******* Get all pages from offset as strings example. *******" );
            Console.WriteLine( $"Get data for resource: { resourceName }" );
            Console.WriteLine( $"Calculated offset of { offset } which is 95 percent of a total count of { totalCount } to avoid paging through potentially lots of pages." );
            Console.WriteLine( "To run with more paging, manually set the offset to a lower value, or reduce the percentage of the total count." );
            int counter = stringList.Count();
            for ( int i = 0; i < counter; i++ )
            {
                var jsonNode = stringList.ElementAt( i );
                Console.WriteLine( $"PAGE { i + 1 }: { stringList.ElementAt( i ) }" );
                Console.WriteLine( $"PAGE { i + 1 } SIZE: { jsonNode.Length }" );
                Console.WriteLine( $"OFFSET: { offset }" );
            }
        }
        catch ( Exception e )
        {
            Console.WriteLine( e.Message );
        }
    }

    /// <summary>
    /// DoGetAllPagesFromOffsetAsJsonsExample
    /// </summary>
    /// <returns></returns>
    private static async Task DoGetAllPagesFromOffsetAsJsonsExample()
    {
        try
        {
            string resourceName = "student-cohorts";
            int totalCount = await proxyClient.GetTotalCountAsync( resourceName );
            // Calculate the offset to be 95% of the totalCount to avoid paging through potentially tons of pages.
            int offset = ( int ) ( totalCount * 0.95 );
            var jsonNodeList = await proxyClient.GetAllPagesFromOffsetAsJArraysAsync( resourceName, "", offset );
            Console.WriteLine( "******* Get all pages from offset as Jsons example. *******" );
            Console.WriteLine( $"Get data for resource: { resourceName }" );
            Console.WriteLine( $"Calculated offset of { offset } which is 95 percent of a total count of { totalCount } to avoid paging through potentially lots of pages." );
            Console.WriteLine( "To run with more paging, manually set the offset to a lower value, or reduce the percentage of the total count." );
            int counter = jsonNodeList.Count();
            for ( int i = 0; i < counter; i++ )
            {
                JArray? jsonNode = jsonNodeList.ElementAt( i );
                Console.WriteLine( $"PAGE { i + 1 }: { jsonNode.ToString() }" );
                Console.WriteLine( $"PAGE { i + 1 } SIZE: { jsonNode.Count() }" );
                Console.WriteLine( $"OFFSET: { offset }" );
            }
        }
        catch ( Exception e )
        {
            Console.WriteLine( e.Message );
        }
    }

    /// <summary>
    /// DoGetPagesExample
    /// </summary>
    /// <returns></returns>
    private static async Task DoGetPagesExample()
    {
        try
        {
            string resourceName = "student-cohorts";
            var ethosResponseList = await proxyClient.GetPagesAsync( resourceName, "", 15, 3 );
            Console.WriteLine( "******* Get some number of pages with page size example. *******" );
            Console.WriteLine( $"Get data for resource: { resourceName }" );
            int counter = ethosResponseList.Count();

            for ( int i = 0; i < counter; i++ )
            {
                var jsonNode = ethosResponseList.ElementAt( i );
                Console.WriteLine( $"PAGE { i + 1 }: { jsonNode.Content }" );
                Console.WriteLine( $"PAGE { i + 1 } SIZE: { jsonNode.Content.Length }" );
            }
        }
        catch ( Exception e )
        {
            Console.WriteLine( e.Message );
        }
    }

    /// <summary>
    /// DoGetPagesAsstringsExample
    /// </summary>
    /// <returns></returns>
    private static async Task DoGetPagesAsstringsExample()
    {
        try
        {
            string resourceName = "student-cohorts";
            var stringList = await proxyClient.GetPagesAsStringsAsync( resourceName, "", 15, 3 );
            Console.WriteLine( "******* Get some number of pages as strings with page size example. *******" );
            Console.WriteLine( $"Get data for resource: { resourceName }" );
            int counter = stringList.Count();
            for ( int i = 0; i < counter; i++ )
            {
                string jsonNode = stringList.ElementAt( i );
                Console.WriteLine( $"PAGE { i + 1 }: { stringList.ElementAt( i ) }" );
                Console.WriteLine( $"PAGE { i + 1 } SIZE: { jsonNode.Length }" );
            }
        }
        catch ( Exception e )
        {
            Console.WriteLine( e.Message );
        }
    }

    /// <summary>
    /// DoGetPagesAsJsonsExample
    /// </summary>
    /// <returns></returns>
    private static async Task DoGetPagesAsJsonsExample()
    {
        try
        {
            string resourceName = "student-cohorts";
            var jsonNodeList = await proxyClient.GetPagesAsJArraysAsync( resourceName, "", 15, 3 );
            Console.WriteLine( "******* Get some number of pages as Jsons with page size example. *******" );
            Console.WriteLine( $"Get data for resource: { resourceName }" );
            int counter = jsonNodeList.Count();

            for ( int i = 0; i < counter; i++ )
            {
                var jsonNode = jsonNodeList.ElementAt( i );
                Console.WriteLine( $"PAGE { i + 1 }: { jsonNode.ToString() }" );
                Console.WriteLine( $"PAGE { i + 1 } SIZE: { jsonNode.Count() }" );
            }
        }
        catch ( Exception e )
        {
            Console.WriteLine( e.Message );
        }
    }

    /// <summary>
    /// DoGetPagesFromOffsetExample
    /// </summary>
    /// <returns></returns>
    private static async Task DoGetPagesFromOffsetExample()
    {
        try
        {
            string resourceName = "student-cohorts";
            int pageSize = 15;
            int offset = 10;
            int numPages = 3;
            var ethosResponseList = await proxyClient.GetPagesFromOffsetAsync( resourceName, "", pageSize, offset, numPages );
            Console.WriteLine( "******* Get some number of pages with page size from offset example. *******" );
            Console.WriteLine( $"Get data for resource: { resourceName }" );
            Console.WriteLine( $"OFFSET: { offset }" );
            int counter = ethosResponseList.Count();

            for ( int i = 0; i < counter; i++ )
            {
                var jsonNode = ethosResponseList.ElementAt( i );
                Console.WriteLine( $"PAGE{ i + 1 }: { jsonNode.Content }" );
                Console.WriteLine( $"PAGE{ i + 1 } SIZE: { jsonNode.Content.Length }" );
            }
            //Console.WriteLine( $"NUM PAGES: { }", ethosResponseList.size() ) );
        }
        catch ( Exception e )
        {
            Console.WriteLine( e.Message );
        }
    }

    /// <summary>
    /// DoGetPagesFromOffsetAsstringsExample
    /// </summary>
    /// <returns></returns>
    private static async Task DoGetPagesFromOffsetAsstringsExample()
    {
        try
        {
            string resourceName = "student-cohorts";
            int pageSize = 15;
            int offset = 10;
            int numPages = 3;
            var stringList = await proxyClient.GetPagesFromOffsetAsStringsAsync( resourceName, "", pageSize, offset, numPages );
            Console.WriteLine( "******* Get some number of pages as strings with page size example. *******" );
            Console.WriteLine( $"Get data for resource: { resourceName }" );
            Console.WriteLine( $"OFFSET: { offset }" );
            int counter = stringList.Count();

            for ( int i = 0; i < counter; i++ )
            {
                var jsonNode = stringList.ElementAt( i );
                Console.WriteLine( $"PAGE { i + 1 }: { jsonNode }" );
                Console.WriteLine( $"PAGE { i + 1 } SIZE: { jsonNode.Length }" );
            }
            //Console.WriteLine( string.format( "NUM PAGES: %s", stringList.size() ) );
        }
        catch ( Exception e )
        {
            Console.WriteLine( e.Message );
        }
    }

    /// <summary>
    /// DoGetPagesFromOffsetAsJsonsExample
    /// </summary>
    /// <returns></returns>
    private static async Task DoGetPagesFromOffsetAsJsonsExample()
    {
        try
        {
            string resourceName = "student-cohorts";
            int pageSize = 15;
            int offset = 10;
            int numPages = 3;
            var jsonNodeList = await proxyClient.GetPagesFromOffsetAsJArraysAsync( resourceName, "", pageSize, offset, numPages );
            Console.WriteLine( "******* Get some number of pages from offset as Jsons with page size example. *******" );
            Console.WriteLine( $"Get data for resource: { resourceName }" );
            Console.WriteLine( $"OFFSET: { offset }" );
            int counter = jsonNodeList.Count();

            for ( int i = 0; i < counter; i++ )
            {
                var jsonNode = jsonNodeList.ElementAt( i );
                Console.WriteLine( $"PAGE {  i + 1 }: {jsonNode.ToString()}" );
                Console.WriteLine( $"PAGE { i + 1 } SIZE: { jsonNode.Count() }" );
            }
            //Console.WriteLine( string.format( "NUM PAGES: %s", jsonNodeList.size() ) );
        }
        catch ( Exception e )
        {
            Console.WriteLine( e.Message );
        }
    }

    /// <summary>
    /// DoGetRowsExample
    /// </summary>
    /// <returns></returns>
    private static async Task DoGetRowsExample()
    {
        try
        {
            string resourceName = "student-cohorts";
            string version = "application/vnd.hedtech.integration.v7.2.0+json";
            int pageSize = 15;
            int numRows = 40;
            int? rowCount = 0;
            var ethosResponseList = await proxyClient.GetRowsAsync( resourceName, version, pageSize, numRows );
            Console.WriteLine( "******* Get some number of rows with page size example. *******" );
            Console.WriteLine( $"Get data for resource: { resourceName }" );

            for ( int i = 0; i < ethosResponseList.Count(); i++ )
            {
                EthosResponse? ethosResponse = ethosResponseList.ElementAt( i );
                JArray? jsonNode = JsonConvert.DeserializeObject( ethosResponse!.Content ) as JArray;
                rowCount += jsonNode?.Count;
                Console.WriteLine( $"PAGE { i + 1 }: { ethosResponse.Content }" );
                Console.WriteLine( $"PAGE { i + 1 } SIZE: { jsonNode?.Count }" );
            }
            Console.WriteLine( $"NUM ROWS: { rowCount }" );
        }
        catch ( Exception e )
        {
            Console.WriteLine( e.Message );
        }
    }

    /// <summary>
    /// DoGetRowsAsstringsExample
    /// </summary>
    /// <returns></returns>
    private static async Task DoGetRowsAsstringsExample()
    {
        try
        {
            string resourceName = "student-cohorts";
            string version = "application/vnd.hedtech.integration.v7.2.0+json";
            int numRows = 40;
            var stringList = await proxyClient.GetRowsAsStringsAsync( resourceName, version, numRows );
            Console.WriteLine( "******* Get some number of rows as strings example. *******" );
            Console.WriteLine( $"Get data for resource: {resourceName}" );
            Console.WriteLine( $"RESPONSE: { string.Join( ',', stringList ) }" );
            Console.WriteLine( $"NUM ROWS: { stringList.Count() }" );
        }
        catch ( Exception e )
        {
            Console.WriteLine( e.Message );
        }
    }

    /// <summary>
    /// DoGetRowsAsJsonsExample
    /// </summary>
    /// <returns></returns>
    private static async Task DoGetRowsAsJsonExample()
    {
        try
        {
            string resourceName = "student-cohorts";
            string version = "application/vnd.hedtech.integration.v7.2.0+json";
            int numRows = 40;
            var jsonNode = await proxyClient.GetRowsAsJArrayAsync( resourceName, version, numRows );
            Console.WriteLine( "******* Get some number of rows as JSON objects example. *******" );
            Console.WriteLine( $"Get data for resource: { resourceName }" );
            Console.WriteLine( $"RESPONSE: { jsonNode.ToString() }" );
            Console.WriteLine( $"NUM ROWS: { jsonNode.Count }" );
        }
        catch ( Exception e )
        {
            Console.WriteLine( e.Message );
        }
    }

    /// <summary>
    /// DoGetRowsFromOffsetExample
    /// </summary>
    /// <returns></returns>
    private static async Task DoGetRowsFromOffsetExample()
    {
        try
        {
            string resourceName = "persons";
            string version = "application/json";
            int pageSize = 300;
            int offset = 0;
            int numRows = 1000;
            int? rowCount = 0;
            var ethosResponseList = await proxyClient.GetRowsFromOffsetAsync( resourceName, version, pageSize, offset, numRows );
            Console.WriteLine( "******* Get some number of rows from offset with page size example. *******" );
            Console.WriteLine( $"Get data for resource: { resourceName }" );
            Console.WriteLine( $"OFFSET: { offset }" );

            for ( int i = 0; i < ethosResponseList.Count(); i++ )
            {
                EthosResponse? ethosResponse = ethosResponseList.ElementAt( i );
                JArray? jsonNode = JsonConvert.DeserializeObject( ethosResponse.Content ) as JArray;
                rowCount += jsonNode?.Count;
                //Uncomment the following line if you wish to print json to the console.
                //Console.WriteLine( $"PAGE { i + 1 }: { ethosResponse.Content }" );
                Console.WriteLine( $"PAGE { i + 1 } SIZE: { jsonNode?.Count }" );
                Console.WriteLine( $"Requested Url: { ethosResponse.RequestedUrl }" );
            }
            Console.WriteLine( $"NUM ROWS: { rowCount }" );
        }
        catch ( Exception e )
        {
            Console.WriteLine( e.Message );
        }
    }

    /// <summary>
    /// DoGetRowsFromOffsetAsstringsExample
    /// </summary>
    /// <returns></returns>
    private static async Task DoGetRowsFromOffsetAsstringsExample()
    {
        try
        {
            string resourceName = "student-cohorts";
            string version = "application/json";
            int offset = 10;
            int numRows = 40;
            var stringList = await proxyClient.GetRowsFromOffsetAsStringsAsync( resourceName, version, offset, numRows );
            Console.WriteLine( "******* Get some number of rows from offset as strings example. *******" );
            Console.WriteLine( $"Get data for resource: { resourceName }" );
            Console.WriteLine( $"OFFSET: { offset }" );
            Console.WriteLine( $"RESPONSE: { string.Join( ',', stringList ) }" );
            Console.WriteLine( $"NUM ROWS: { stringList.Count() }" );
        }
        catch ( Exception e )
        {
            Console.WriteLine( e.Message );
        }
    }

    /// <summary>
    /// DoGetRowsFromOffsetAsJsonsExample
    /// </summary>
    /// <returns></returns>
    private static async Task DoGetRowsFromOffsetAsJsonsExample()
    {
        try
        {
            string resourceName = "student-cohorts";
            string version = "application/json";
            int offset = 10;
            int numRows = 40;
            var jsonNode = await proxyClient.GetRowsFromOffsetAsJArrayAsync( resourceName, version, offset, numRows );
            Console.WriteLine( "******* Get some number of rows from offset as JSON objects example. *******" );
            Console.WriteLine( $"Get data for resource: { resourceName }" );
            Console.WriteLine( $"OFFSET: { offset }" );
            Console.WriteLine( $"RESPONSE: { jsonNode.ToString() }" );
            Console.WriteLine( $"NUM ROWS: { jsonNode.Count }" );
        }
        catch ( Exception e )
        {
            Console.WriteLine( e.Message );
        }
    }

    /// <summary>
    /// An example of Create, Read, Update, and Delete using the EthosProxyClient.
    /// </summary>
    /// <returns>an awaitable task.</returns>
    private static async Task DoCrudExample()
    {
        EthosResponseConverter? converter = new EthosResponseConverter();
        try
        {
            IEnumerable<EthosResponse> responses = await proxyClient.GetRowsAsync( "persons", "", 1, 1 );
            EthosResponse? firstInList = responses.ElementAt( 0 );
            JToken? person = converter.ToJArray( firstInList ).ElementAt( 0 );
            string personId = person [ "id" ].ToString();

            JObject? personHold = new JObject();
            personHold [ "id" ] = "00000000-0000-0000-0000-000000000000";
            personHold [ "startOn" ] = DateTime.Now.ToString( "yyyy-MM-ddThh:mm:ssZ" );

            JObject? personForPersonHold = new JObject();
            personForPersonHold [ "id" ] = personId;
            personHold [ "person" ] = personForPersonHold;
            JObject? categoryForPersonHold = new JObject();
            categoryForPersonHold [ "category" ] = "financial";
            personHold [ "type" ] = categoryForPersonHold;

            EthosResponse? response = await proxyClient.PostAsync( "person-holds", personHold );
            Console.WriteLine( "Created a 'person-holds' record:" );
            Console.WriteLine( response.Content );
            Console.WriteLine();

            // get the ID of the new record
            JObject? personHoldResponse = converter.ToJObjectSingle( response );
            string newId = personHoldResponse [ "id" ].ToString();

            // change the date on the person-hold record, and send a put request to update it
            personHold.Remove( "id" );
            DateTime newHoldEnd = DateTime.Now.AddDays( 1 );
            personHold [ "startOn" ] = newHoldEnd.ToString( "yyyy-MM-ddThh:mm:ssZ" );
            response = await proxyClient.PutAsync( "person-holds", newId, personHold );
            Console.WriteLine( $"Successfully updated person-holds record {newId}" );

            // delete the record
            await proxyClient.DeleteAsync( "person-holds", newId );
            Console.WriteLine( $"Successfully deleted person-holds record {newId}" );

            // attempt to get the formerly created, now deleted, record, and make sure it is no longer there.
            try
            {
                await proxyClient.GetByIdAsync( "person-holds", newId );
            }
            catch
            {
                Console.WriteLine( $"Failed to get person-holds record {newId}.  The delete was successful." );
            }

        }
        catch ( Exception e )
        {
            Console.WriteLine( "An error occured while performing the update ", e.Message );
            Console.WriteLine( e.StackTrace );
        }
    }

    /// <summary>
    /// SimplePersonsIterationExample
    /// </summary>
    /// <returns>Task</returns>
    private static async Task DoSimplePersonsIterationExample()
    {
        JArray? persons = await proxyClient.GetAsJArrayAsync( "persons", string.Empty );
        foreach ( JToken person in persons )
        {
            string id = person [ "id" ].ToString();
            string fullName = person [ "names" ] [ 0 ] [ "fullName" ].ToString();
            Console.WriteLine( $"{fullName} has a person ID of {id}" );
        }
    }

    /// <summary>
    /// GET example for student-cohorts
    /// </summary>
    /// <returns></returns>
    private static async Task DoGetStudentCohortAsync()
    {
        try
        {
            var response = await proxyClient.GetAsync<IEnumerable<StudentCohorts_V7_2_0>>( "student-cohorts" );

            if ( response != null )
            {
                Console.WriteLine( "" );
                Console.WriteLine( string.Format( "{0, -40} {1, -20}{2, -11}{3, -40}{4, -60}", "ID", "COHORT-TYPE", "CODE", "TITLE", "DESCRIPTION" ) );
                foreach ( StudentCohorts_V7_2_0 item in response.Dto )
                {
                    Console.WriteLine( string.Format( "{0, -40} {1, -20}{2, -11}{3, -40}{4, -60}", item.Id, item.StudentCohortType.ToString().Trim(), item.Code.Trim(), item.Title.Trim(), item.Description ?? "No Description" ) );
                }
            }
        }
        catch ( Exception ex )
        {
            Console.WriteLine( "An error occured while performing the GET ", ex.Message );
            Console.WriteLine( ex.StackTrace );
        }
    }

    /// <summary>
    /// DoGetRowsAsJsonsExample
    /// </summary>
    /// <returns></returns>
    private static async Task DoGetRowsAsJsonStronglyTypedExample()
    {
        try
        {
            string resourceName = "term-codes";
            string version = "application/json";
            int numRows = 10;
            var jsonNode = await proxyClient.GetRowsAsJArrayAsync( resourceName, version, numRows );
            var termCodes = JsonConvert.DeserializeObject <IEnumerable<TermCodesV100GetRequest>>( jsonNode.ToString() );
            Console.WriteLine( "******* Get some number of rows as JSON and convert to strongly typed objects example. *******" );
            Console.WriteLine( $"Get data for resource: { resourceName }" );
            Console.WriteLine( $"NUM ROWS: { termCodes!.ToArray().Count() }" );
        }
        catch ( Exception e )
        {
            Console.WriteLine( e.Message );
        }
    }

    /// <summary>
    /// Person holds full CRUD example.
    /// </summary>
    /// <returns></returns>
    private static async Task DoFullCrudWithStronglyTypedAsync()
    {
        try
        {
            Persons_V12_3_0? person = await GetPersonAsync();
            string? personId = person?.Id.ToString();

            // Populate person holds record.
            PersonHolds_V6_0? personHold1 = new PersonHolds_V6_0
            {
                Id = "00000000-0000-0000-0000-000000000000",
                StartOn = DateTimeOffset.Now,
                Person = new GuidObject2 { Id = personId! },
                PersonHoldTypeType = new PersonHoldTypeType() { PersonHoldCategory = PersonHoldCategoryTypes.Financial }
            };

            // POST
            EthosResponse? response = await proxyClient.PostAsync<PersonHolds_V6_0>( "person-holds", personHold1 );

            // PRINT
            var dto = response.Dto as PersonHolds_V6_0;
            Console.WriteLine( "Created a 'person-holds' record:" );
            Console.WriteLine( dto!.Id );
            Console.WriteLine();

            string newId = response.Dto.Id.ToString();
            DateTimeOffset? newHoldEnd = DateTimeOffset.Now.AddDays( 1 );
            personHold1.StartOn = newHoldEnd;
            // PUT
            response = await proxyClient.PutAsync<PersonHolds_V6_0>( "person-holds", dto, newId );
            Console.WriteLine( $"Successfully updated person-holds record {newId}" );

            // DELETE
            await proxyClient.DeleteAsync( "person-holds", newId );
            Console.WriteLine( $"Successfully deleted person-holds record {newId}" );

            // attempt to get the formerly created, now deleted, record, and make sure it is no longer there.
            try
            {
                await proxyClient.GetByIdAsync( "person-holds", newId );
            }
            catch
            {
                Console.WriteLine( $"Failed to get person-holds record {newId}.  The delete was successful." );
            }

        }
        catch ( Exception e )
        {
            Console.WriteLine( "An error occured while performing the update ", e.Message );
            Console.WriteLine( e.StackTrace );
        }
    }

    /// <summary>
    /// How to perform http GET using a strongly typed object.
    /// </summary>
    /// <returns></returns>
    private static async Task DoGetTermCodesAsync()
    {
        try
        {
            var response = await proxyClient.GetAsync<IEnumerable<TermCodesV100GetRequest>>( "term-codes" );

            if ( response != null )
            {
                Console.WriteLine( "" );
                foreach ( var item in ( IEnumerable<TermCodesV100GetRequest> ) response.Dto )
                {
                    Console.WriteLine( $"Activity Date: { item.ActivityDate }, CODE: { item.Code }, DESC: { item.Desc } " );
                }
            }
        }
        catch ( Exception ex )
        {
            Console.WriteLine( ex.Message );
        }
    }

    /// <summary>
    /// Helper method using GET to retrieve collection of person records and choosing a random records from the collection.
    /// </summary>
    /// <returns></returns>
    private static async Task<Persons_V12_3_0> GetPersonAsync()
    {
        Random? random = new();
        int num = random.Next( 0, 499 );
        EthosResponse? responses = await proxyClient.GetAsync<IEnumerable<Persons_V12_3_0>>( "persons", "", num, 1 );
        Persons_V12_3_0? person = ( responses.Dto as IEnumerable<Persons_V12_3_0> )!.FirstOrDefault();
        return person!;
    }

    private static void Print( string exampleNumber, string filter )
    {
        Console.WriteLine( $"{exampleNumber}\t: {filter}" );
    }

    #endregion

    #region Helper Method
    /// <summary>
    /// PrintHeaders
    /// </summary>
    /// <param name="ethosResponse"></param>
    private static void PrintHeaders( EthosResponse ethosResponse )
    {
        Console.WriteLine( $"Header: { ethosResponse.GetHeader( EthosProxyClient.HDR_DATE ) }" );
        Console.WriteLine( $"Header: { ethosResponse.GetHeader( EthosProxyClient.HDR_CONTENT_TYPE ) }" );
        Console.WriteLine( $"Header: { ethosResponse.GetHeader( EthosProxyClient.HDR_X_TOTAL_COUNT ) }" );
        Console.WriteLine( $"Header: { ethosResponse.GetHeader( EthosProxyClient.HDR_APPLICATION_CONTEXT ) }" );
        Console.WriteLine( $"Header: { ethosResponse.GetHeader( EthosProxyClient.HDR_X_MAX_PAGE_SIZE ) }" );
        Console.WriteLine( $"Header: { ethosResponse.GetHeader( EthosProxyClient.HDR_X_MEDIA_TYPE ) }" );
        Console.WriteLine( $"Header: { ethosResponse.GetHeader( EthosProxyClient.HDR_HEDTECH_ETHOS_INTEGRATION_APPLICATION_ID ) }" );
        Console.WriteLine( $"Header: { ethosResponse.GetHeader( EthosProxyClient.HDR_HEDTECH_ETHOS_INTEGRATION_APPLICATION_NAME ) }" );
    }

    #endregion
}

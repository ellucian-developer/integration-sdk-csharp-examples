/*
 * ******************************************************************************
 *   Copyright  2022 Ellucian Company L.P. and its affiliates.
 * ******************************************************************************
 */

using Ellucian.Ethos.Integration.Client;
using Ellucian.Ethos.Integration.Client.Extensions;
using Ellucian.Ethos.Integration.Client.Proxy.Filter;
using Ellucian.Generated.BpApi.PersonCommentsV100PostRequest;
using Ellucian.Generated.BpApi.PersonCommentsV100PostResponse;
using Ellucian.Generated.BpApi.PersonCommentsV100PutRequest;
using Ellucian.Generated.BpApi.PersonCommentsV100PutResponse;
using Ellucian.Generated.BpApi.PersonSearchV100GetRequest;
using Ellucian.Generated.BpApi.TermCodesV100GetRequest;

namespace Ellucian.Examples;

public class BannerBPAPIExample : ExampleBase
{

    /// <summary>
    /// Calls all Banner BPAPI's.
    /// </summary>
    /// <returns></returns>
    public static async Task Run()
    {
        BuildEthosProxyClient();
        await ExampleGetTermCodesAsync();
        await ExampleFullCrudWithStronglyTypedAsync();
    }

    /// <summary>
    /// How to perform http GET using a strongly typed object.
    /// </summary>
    /// <returns></returns>
    private static async Task ExampleGetTermCodesAsync()
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
    /// This example illustrate hot to use strongly typed object to perform a GET/POST/PUT operation.
    /// </summary>
    /// <returns></returns>
    private static async Task ExampleFullCrudWithStronglyTypedAsync()
    {
        BuildEthosFilterClient();
        var resourceName = "person-search";
        var filterMap = new FilterMap() { };
        var filter = filterMap.WithParameterPair( "lastName", "abbe" ).Build();

        //Use filter client to perform a serch using http GET to find records.
        var response = await filterClient.GetWithFilterMapAsync<IEnumerable<PersonSearchV100GetRequest>>( resourceName, filter );
        int num = GetRandomNumber( ( ( IEnumerable<PersonSearchV100GetRequest> ) response.Dto ).Count() );

        try
        {
            //Select a random record from searched records collection.
            PersonSearchV100GetRequest? searchedRecord = ( ( IEnumerable<PersonSearchV100GetRequest> ) response.Dto ).ToList() [ num ];
            Console.WriteLine( $"{ searchedRecord.LastName } { searchedRecord.Id }" );

            //Create a strongly typed object for POST.
            PersonCommentsV100PostRequest pcPostReq = new PersonCommentsV100PostRequest()
            {
                Id = searchedRecord.Id,
                CmttCode = "100",
                ConfidentialInd = "s",
                ContactDate = DateTime.Now,
                Date = DateTime.Now,
                OrigCode = "CCON",
                Text = $"POST comment 1 from C-SHarp SDK Example. PERSON RECORD: { searchedRecord.Id }: { searchedRecord.LastName }, { searchedRecord.FirstName }",
                TextNar = "Testing through C-Sharp SDK"
            };

            //Perform POST.
            EthosResponse ethosPostResponse = await filterClient.PostAsync<PersonCommentsV100PostRequest>( "person-comments", pcPostReq );
            var pcResps = ethosPostResponse.Deserialize<IEnumerable<PersonCommentsV100PostResponse>>();
            foreach ( PersonCommentsV100PostResponse pcResp in pcResps )
            {
                Console.WriteLine( $"Contact Date: {pcResp.ContactDate}, Text: {pcResp.Text}, Text Nar: {pcResp.TextNar}" );
            }

            //Create a strongly typed object for PUT.
            PersonCommentsV100PutRequest putReq = new PersonCommentsV100PutRequest()
            {
                Id = searchedRecord.Id,
                CmttCode = "100",
                ConfidentialInd = "s",
                ContactDate = DateTime.Now,
                Date = DateTime.Now,
                OrigCode = "CCON",
                Text = $"PUT comment 2 from C-SHarp SDK Example. PERSON RECORD: { searchedRecord.Id }: { searchedRecord.LastName }, { searchedRecord.FirstName }",
                TextNar = "Testing through C-Sharp SDK"
            };

            //Perform PUT
            EthosResponse ethosPutResponse = await filterClient.PutAsync<PersonCommentsV100PutRequest>( "person-comments", putReq );
            var putResps = ethosPutResponse.Deserialize<IEnumerable<PersonCommentsV100PutResponse>>();
            foreach ( PersonCommentsV100PutResponse putResp in putResps )
            {
                Console.WriteLine( $"Contact Date: {putResp.ContactDate}, Text: {putResp.Text}, Text Nar: {putResp.TextNar}" );
            }
        }
        catch ( Exception ex )
        {
            Console.WriteLine( ex.Message );
        }
    }
}

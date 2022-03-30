﻿/*
 * ******************************************************************************
 *   Copyright  2022 Ellucian Company L.P. and its affiliates.
 * ******************************************************************************
 */

using Ellucian.Ethos.Integration.Client;
using Ellucian.Ethos.Integration.Client.Filter.Extensions;
using Ellucian.Ethos.Integration.Client.Proxy;
using Ellucian.Ethos.Integration.Client.Proxy.Filter;

namespace Ellucian.Examples;

/// <summary>
/// This class provides examples of calling API's using filter syntax for version before V8 with old syntax and after V8 with new syntax.
/// </summary>
public class EthosFilterQueryClientExample : ExampleBase
{
    /// <summary>
    /// Uncomment this Main method and comment the Main method in other examples to GetChangeNotificationsAsync().
    /// You can only have one Main method in the same project.</summary>
    /// <param name="args">Pass api key from the command line, args[0] will then contain the api key.</param>
    /// <returns>Task</returns>
    public static async Task Run()
    {
        BuildEthosFilterClient();
        CriteriaExamples();
        NamedQueryExamples();
        await GetNamesUsingCriteriaFilterStringAsync();
        await GetUsingCriteriaFilterRolesAsync();
        await GetUsingCriteriaFilterCredentialsAsync();
        await GetUsingCriteriaFilterAlternateCredentialsAsync();
        await GetUsingCriteriaFilterEmailsAsync();
        await GetUsingCriteriaFilterAdmissionDecisionsDecidedOnAsync();
        await GetUsingCriteriaFilterCoursesInstructionalMethodDetailsAsync();
        await GetUsingFilterMapAsync();
        await GetPagesUsingCriteriaFilterValuesAsync();
        await GetPagesFromOffsetUsingCriteriaFilterValuesAsync();
        await GetPagesUsingFilterMapValuesAsync();
        await GetPagesUsingNamedQueryAsync();
    }

    #region All Examples
    /// <summary>
    /// Various Examples of how criteria filter can be built.
    /// </summary>
    /// <returns></returns>
    private static void CriteriaExamples()
    {
        //?criteria={"lastName":"Smith"}
        var filter = new CriteriaFilter().WithSimpleCriteria( "lastName", "Smith" ).BuildCriteria();
        Print( "Criteria1", filter );

        //?criteria={"names":[{"firstName":"John","lastName":"smith"}]}
        filter = new CriteriaFilter().WithArray( "names", new CriteriaFilter().WithSimpleCriteria( "firstName", "John" ).WithSimpleCriteria( "lastName", "smith" ) ).BuildCriteria();
        Print( "Criteria2_4", filter );

        //?criteria={"someName":{"anotherName":{"names":{"lastName":"Smith"}}}}
        filter = new CriteriaFilter().WithSimpleCriteria( "names", ("lastName", "Smith") ).WithSimpleCriteria( "anotherName" ).WithSimpleCriteria( "someName" ).BuildCriteria();
        Print( "Criteria3-a", filter );

        //?criteria={"someName":{"anotherName":{"names":{"lastName":"Smith"}}}}
        filter = new CriteriaFilter().WithSimpleCriteria( "someName", new CriteriaFilter().WithSimpleCriteria( "anotherName", new CriteriaFilter().WithSimpleCriteria( "names", ("lastName", "Smith") ) ) ).BuildCriteria();
        Print( "Criteria3-b", filter );

        //?criteria={"statuses":["active","approved"]}
        filter = new CriteriaFilter().WithArray( "statuses", new [] { "active", "approved" } ).BuildCriteria();
        Print( "Criteria5", filter );

        //?criteria={"academicLevels":[{"id":"11111111-1111-1111-1111-111111111111"},{"id":"11111111-1111-1111-1111-111111111112"}]}
        filter = new CriteriaFilter().WithArray( "academicLevels", ("id", "11111111-1111-1111-1111-111111111111"), ("id", "11111111-1111-1111-1111-111111111112") ).BuildCriteria();
        Print( "Criteria6", filter );

        //?criteria={"authors":[{"person":{"id":"11111111-1111-1111-1111-111111111111"}}]}
        filter = new CriteriaFilter().WithArray( "authors", new CriteriaFilter().WithSimpleCriteria( "person", ("id", "11111111-1111-1111-1111-111111111111") ) ).BuildCriteria();
        Print( "Criteria7", filter );

        //?criteria={"names":{"personalNames":[{"title":"Mr."},{"title":"Mr"}]}}
        filter = new CriteriaFilter().WithSimpleCriteria( "names", new CriteriaFilter().WithArray( "personalNames", ("title", "Mr."), ("title", "Mr") ) ).BuildCriteria();
        Print( "Criteria8", filter );

        //?criteria={"credentials":[{"type":{"id":"11111111-1111-1111-1111-111111111111"}},{"value":"bannerId"}]}
        filter = new CriteriaFilter().WithArray( "credentials",
                     new CriteriaFilter().WithSimpleCriteria( "type", ("id", "11111111-1111-1111-1111-111111111111") ),
                     new CriteriaFilter().WithSimpleCriteria( "value", "bannerId" ) ).BuildCriteria();
        Print( "Criteria9", filter );

        //?criteria={"academicLevels":[{"id":"11111111-1111-1111-1111-111111111111","firstName":"John"},{"id":"11111111-1111-1111-1111-111111111112","lastName":"Smith"}]}
        Dictionary<string, object> dict = new Dictionary<string, object>();
        dict.Add( "id", "11111111-1111-1111-1111-111111111111" );
        dict.Add( "firstName", "John" );
        Dictionary<string, object> dict1 = new Dictionary<string, object>();
        dict1.Add( "id", "11111111-1111-1111-1111-111111111112" );
        dict1.Add( "lastName", "Smith" );
        filter = new CriteriaFilter().WithArray( "academicLevels", dict, dict1 ).BuildCriteria();
        Print( "Criteria10", filter );

        //?criteria={"solicitors":[{"solicitor":{"constituent":{"person":{"id":"11111111-1111-1111-1111-111111111111"}}}},{"solicitor":{"constituent":{"person":{"id":"11111111-1111-1111-1111-111111111111"}}}}]}
        var solicitors = new CriteriaFilter().WithSimpleCriteria( "constituent", new CriteriaFilter().WithSimpleCriteria( "person", ("id", "11111111-1111-1111-1111-111111111111") ) )
                            .WithSimpleCriteria( "solicitor" );
        filter = new CriteriaFilter().WithArray( "solicitors", solicitors, solicitors ).BuildCriteria();
        Print( "Criteria11", filter );
        //Or ?criteria={"solicitors":[{"solicitor":{"constituent":{"person":{"id":"11111111-1111-1111-1111-111111111111"}}}}]}
        filter = new CriteriaFilter().WithArray( "solicitors",
                    new CriteriaFilter().WithSimpleCriteria( "solicitor",
                        new CriteriaFilter().WithSimpleCriteria( "constituent",
                            new CriteriaFilter().WithSimpleCriteria( "person", ("id", "11111111-1111-1111-1111-111111111111") ) ) ) ).BuildCriteria();
        Print( "Criterias11", filter );

        //?criteria={"person":[{"names":{"personalNames":{"firstName":"John"}}},{"names":{"personalNames":{"firstName":"Johny"}}}]}
        var myName = new CriteriaFilter().WithSimpleCriteria( "names", new CriteriaFilter().WithSimpleCriteria( "personalNames", ("firstName", "John") ) );
        var myName2 = new CriteriaFilter().WithSimpleCriteria( "names", new CriteriaFilter().WithSimpleCriteria( "personalNames", ("firstName", "Johny") ) );
        filter = new CriteriaFilter().WithArray( "person", myName, myName2 ).BuildCriteria();
        Print( "Criteria12", filter );

        //?criteria={"startOn":{"$eq":"2019-04-22T10:03:07+00:00"}}
        filter = new CriteriaFilter().WithSimpleCriteria( "startOn", ("$eq", "2019-04-22T10:03:07+00:00") ).BuildCriteria();
        Print( "Criteria13", filter );

        //?criteria={"ethos":{"resources":["persons","organizations"]}}
        filter = new CriteriaFilter().WithSimpleCriteria( "resources", new [] { "persons", "organizations" } ).WithSimpleCriteria( "ethos" ).BuildCriteria();
        Print( "Criteria14", filter );

        //?criteria={"credentials":[{"type":"bannerId","value":"SomeUser"}]}
        filter = new CriteriaFilter().WithArray( "credentials", new CriteriaFilter().WithSimpleCriteria( "type", "bannerId" ).WithSimpleCriteria( "value", "SomeUser" ) ).BuildCriteria();
        Print( "Criteria15", filter );

        //?criteria={"startOn":{"year":"2021","month":"April"}}
        filter = new CriteriaFilter().WithSimpleCriteria( "startOn", new CriteriaFilter().WithSimpleCriteria( "year", "2021" ).WithSimpleCriteria( "month", "April" ) ).BuildCriteria();
        Print( "Criteria16", filter );


        //Criteria17      : ?criteria={"ethos":{"resources":["persons","organizations"]}} 
        filter = new CriteriaFilter().WithSimpleCriteria( "resources", new [] { "persons", "organizations" } ).WithSimpleCriteria( "ethos" ).BuildCriteria();
        Print( "Criteria17", filter );
    }

    /// <summary>
    /// Various examples of how named query is built.
    /// </summary>
    private static void NamedQueryExamples()
    {
        //?registrationStatusesByAcademicPeriod={"statuses":[{"detail":{"id":"11111111-1111-1111-1111-111111111111"}},{"detail":{"id":"11111111-1111-1111-1111-111111111112"}}],"academicPeriod":{"id":"11111111-1111-1111-1111-111111111113"}}
        var filter = new NamedQueryFilter( "registrationStatusesByAcademicPeriod" )
                        .WithNamedQuery( "statuses", ("detail", "id", "11111111-1111-1111-1111-111111111111"), ("detail", "id", "11111111-1111-1111-1111-111111111112") )
                        .WithNamedQuery( "academicPeriod", "id", "11111111-1111-1111-1111-111111111113" )
                        .BuildNamedQuery();
        Print( "NamedQuery1", filter );

        //?instructor={"instructor":{"id":"11111111-1111-1111-1111-111111111111"}}
        filter = new NamedQueryFilter( "instructor" )
                    .WithNamedQuery( "instructor", "id", "11111111-1111-1111-1111-111111111111" )
                    .BuildNamedQuery();
        Print( "NamedQuery2", filter );

        //?keywordSearch={"keywordSearch":"Origins"}
        filter = new NamedQueryFilter( "keywordSearch" )
                    .WithNamedQuery( "keywordSearch", "Origins" )
                    .BuildNamedQuery();
        Print( "NamedQuery3", filter );
    }

    /// <summary>
    /// Creates : ?criteria={"names":[{"firstName":"John","lastName":"smith"}]}
    /// </summary>
    /// <returns></returns>
    private static async Task GetNamesUsingCriteriaFilterStringAsync()
    {
        Console.WriteLine( "******* GetNamesUsingCriteriaFilterStringAsync() using filter string *******" );
        string resource = "persons";
        string version = "application/vnd.hedtech.integration.v12.3.0+json";
        string criteriaFilterStr = "?criteria={\"names\":[{\"firstName\":\"John\",\"lastName\":\"Smith\"}]}";

        Console.WriteLine( $"Criteria: { criteriaFilterStr }" );
        try
        {
            EthosResponse ethosResponse = await filterClient.GetWithCriteriaFilterAsync( resource, version, criteriaFilterStr );
            Console.WriteLine( $"REQUESTED URL: { ethosResponse.RequestedUrl }" );
            Console.WriteLine( $"Number of resources returned: { ethosResponse.GetContentCount() }\r\n" );
            Console.WriteLine( ethosResponse.GetContentAsJson().ToString() );
        }
        catch ( Exception e )
        {
            Console.WriteLine( e.Message );
        }
    }

    /// <summary>
    /// Creates : ?criteria={"roles":[{"role":"instructor"},{"role":"advisor"}]}
    /// </summary>
    /// <returns></returns>
    private static async Task GetUsingCriteriaFilterRolesAsync()
    {
        Console.WriteLine( "******* GetUsingCriteriaFilterRolesAsync() using CriteriaFilter *******" );
        string resource = "persons";
        string version = "application/vnd.hedtech.integration.v12.3.0+json";
        string criteriaFilterStr = new CriteriaFilter()
                                            .WithArray( "roles", ("role", "instructor"), ("role", "advisor") )
                                            .BuildCriteria();
        Console.WriteLine( $"Criteria: { criteriaFilterStr }" );

        try
        {
            EthosResponse ethosResponse = await filterClient.GetWithCriteriaFilterAsync( resource, version, criteriaFilterStr );
            Console.WriteLine( $"REQUESTED URL: { ethosResponse.RequestedUrl }" );
            Console.WriteLine( $"Number of resources returned: { ethosResponse.GetContentCount() }\r\n" );
            Console.WriteLine( ethosResponse.GetContentAsJson().ToString() );
        }
        catch ( Exception e )
        {
            Console.WriteLine( e.Message );
        }
    }

    /// <summary>
    /// Creates: ?criteria={"credentials":[{"type":"bannerSourcedId","value":"684"}]}
    /// </summary>
    /// <returns></returns>
    private static async Task GetUsingCriteriaFilterCredentialsAsync()
    {
        Console.WriteLine( "******* GetUsingCriteriaFilterCredentialsAsync() using values *******" );
        string resource = "persons";
        string version = "application/vnd.hedtech.integration.v12.3.0+json";
        string criteriaFilterStr = new CriteriaFilter()
                                            .WithArray( "roles", ("role", "instructor"), ("role", "advisor") )
                                            .BuildCriteria();
        Console.WriteLine( $"Criteria: { criteriaFilterStr }" );

        try
        {
            EthosResponse ethosResponse = await filterClient.GetWithCriteriaFilterAsync( resource, version, criteriaFilterStr );
            Console.WriteLine( $"REQUESTED URL: { ethosResponse.RequestedUrl }" );
            Console.WriteLine( $"Number of resources returned: { ethosResponse.GetContentCount() }\r\n" );
            Console.WriteLine( ethosResponse.GetContentAsJson().ToString() );
        }
        catch ( Exception e )
        {
            Console.WriteLine( e.Message );
        }
    }

    /// <summary>
    /// Creates: ?criteria={"alternativeCredentials":[{"type":{"id":"11111111-1111-1111-1111-111111111111"},"value":"abcd"}]}
    /// </summary>
    /// <returns></returns>
    private static async Task GetUsingCriteriaFilterAlternateCredentialsAsync()
    {
        Console.WriteLine( "******* GetUsingCriteriaFilterAlternateCredentialsAsync() using values *******" );
        string resource = "persons";
        string version = "application/vnd.hedtech.integration.v12.3.0+json";
        string id = "11111111-1111-1111-1111-111111111111";
        string value = "abcd";
        string criteriaFilterStr = new CriteriaFilter().WithArray( "alternativeCredentials",
                                   new CriteriaFilter().WithSimpleCriteria( "type", new CriteriaFilter().WithSimpleCriteria( "id", id ) ).WithSimpleCriteria( "value", value ) )
                                   .BuildCriteria();
        Console.WriteLine( $"Criteria: { criteriaFilterStr }" );

        try
        {
            EthosResponse ethosResponse = await filterClient.GetWithCriteriaFilterAsync( resource, version, criteriaFilterStr );
            Console.WriteLine( $"REQUESTED URL: { ethosResponse.RequestedUrl }" );
            Console.WriteLine( $"Number of resources returned: { ethosResponse.GetContentCount() }\r\n" );
            Console.WriteLine( ethosResponse.GetContentAsJson().ToString() );
        }
        catch ( Exception e )
        {
            Console.WriteLine( e.Message );
        }
    }

    /// <summary>
    /// Creates: ?criteria={"emails":[{"address":"abcd@xyz.com"},{"address":"testing123@testing.com"}]}
    /// </summary>
    /// <returns></returns>
    private static async Task GetUsingCriteriaFilterEmailsAsync()
    {
        Console.WriteLine( "******* GetUsingCriteriaFilterEmailsAsync() using values *******" );
        string resource = "persons";
        string version = "application/vnd.hedtech.integration.v12.3.0+json";
        string email1 = "abcd@xyz.com";
        string email2 = "testing123@testing.com";
        string criteriaFilterStr = new CriteriaFilter()
                                    .WithArray( "emails", ("address", email1), ("address", email2) )
                                    .BuildCriteria();
        Console.WriteLine( $"Criteria: { criteriaFilterStr }" );

        try
        {
            EthosResponse ethosResponse = await filterClient.GetWithCriteriaFilterAsync( resource, version, criteriaFilterStr );
            Console.WriteLine( $"REQUESTED URL: { ethosResponse.RequestedUrl }" );
            Console.WriteLine( $"Number of resources returned: { ethosResponse.GetContentCount() }\r\n" );
            Console.WriteLine( ethosResponse.GetContentAsJson().ToString() );
        }
        catch ( Exception e )
        {
            Console.WriteLine( e.Message );
        }
    }

    /// <summary>
    /// Creates: ?criteria={"decidedOn":{"$lte":"2003-03-22T18:57:25Z"}}
    /// </summary>
    /// <returns></returns>
    private static async Task GetUsingCriteriaFilterAdmissionDecisionsDecidedOnAsync()
    {
        Console.WriteLine( "******* GetUsingCriteriaFilterAdmissionDecisionsDecidedOnAsync() using values *******" );
        string resource = "admission-decisions";
        string decidedOn = "2003-03-22T18:57:25Z";
        string criteriaFilterStr = new CriteriaFilter()
                                    .WithSimpleCriteria( "decidedOn", ("$lte", decidedOn) )
                                    .BuildCriteria();
        Console.WriteLine( $"Criteria: { criteriaFilterStr }" );

        try
        {
            EthosResponse ethosResponse = await filterClient.GetWithCriteriaFilterAsync( resource, criteriaFilterStr );
            Console.WriteLine( $"REQUESTED URL: { ethosResponse.RequestedUrl }" );
            Console.WriteLine( $"Number of resources returned: { ethosResponse.GetContentCount() }\r\n" );
            Console.WriteLine( ethosResponse.GetContentAsJson().ToString() );
        }
        catch ( Exception e )
        {
            Console.WriteLine( e.Message );
        }
    }

    /// <summary>
    /// Creates: ?criteria={"instructionalMethodDetails":[{"instructionalMethod":{"id":"11111111-1111-1111-1111-111111111111"}},{"instructionalMethod":{"id":"11111111-1111-1111-1111-111111111112"}}]}
    /// </summary>
    /// <returns></returns>
    private static async Task GetUsingCriteriaFilterCoursesInstructionalMethodDetailsAsync()
    {
        Console.WriteLine( "******* GetUsingCriteriaFilterCoursesInstructionalMethodDetailsAsync() using values *******" );
        string resource = "courses";
        var id1 = "11111111-1111-1111-1111-111111111111";
        var id2 = "11111111-1111-1111-1111-111111111112";
        string criteriaFilterStr = new CriteriaFilter()
                                    .WithArray( "instructionalMethodDetails",
                                    new CriteriaFilter().WithSimpleCriteria( "instructionalMethod", ("id", id1) ),
                                    new CriteriaFilter().WithSimpleCriteria( "instructionalMethod", ("id", id2) ) )
                                    .BuildCriteria();
        Console.WriteLine( $"Criteria: { criteriaFilterStr }" );

        try
        {
            EthosResponse ethosResponse = await filterClient.GetWithCriteriaFilterAsync( resource, criteriaFilterStr );
            Console.WriteLine( $"REQUESTED URL: { ethosResponse.RequestedUrl }" );
            Console.WriteLine( $"Number of resources returned: { ethosResponse.GetContentCount() }\r\n" );
            Console.WriteLine( ethosResponse.GetContentAsJson().ToString() );
        }
        catch ( Exception e )
        {
            Console.WriteLine( e.Message );
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private static async Task GetUsingFilterMapAsync()
    {
        Console.WriteLine( "******* GetWithFilterMapAsync() using filterMap *******" );
        string resource = "persons";
        string version = "application/vnd.hedtech.integration.v6+json";
        string filterKey = "firstName";
        string filterValue = "John";

        try
        {
            FilterMap filterMap = new FilterMap()
                                      .WithParameterPair( filterKey, filterValue )
                                      .WithParameterPair( "lastName", "Smith" )
                                      .Build();
            EthosResponse ethosResponse = await filterClient.GetWithFilterMapAsync( resource, version, filterMap );
            Console.WriteLine( $"REQUESTED URL: { ethosResponse.RequestedUrl }" );
            Console.WriteLine( $"Number of resources returned: { ethosResponse.GetContentCount() }\r\n" );
            Console.WriteLine( ethosResponse.GetContentAsJson().ToString() );
        }

        catch ( Exception e )
        {
            Console.WriteLine( e.Message );
        }
    }

    private static async Task GetPagesUsingCriteriaFilterValuesAsync()
    {
        Console.WriteLine( "******* GetPagesWithCriteriaFilterAsync() using criteria filter *******" );
        string resource = "persons";
        string version = "application/vnd.hedtech.integration.v12.3.0+json";
        string criteriaSetName = "names";
        string criteriaKey = "firstName";
        string criteriaValue = "John";
        int pageSize = 50;

        try
        {
            var criteriaFilter = new CriteriaFilter().WithArray( criteriaSetName, (criteriaKey, criteriaValue) );
            Console.WriteLine( criteriaFilter.BuildCriteria() );
            List<EthosResponse> ethosResponseList = await filterClient.GetPagesWithCriteriaFilterAsync( resource, version, criteriaFilter, pageSize ) as List<EthosResponse>;
            Console.WriteLine( $"Number of pages returned: {ethosResponseList.Count}" );
            foreach ( EthosResponse ethosResponse in ethosResponseList )
            {
                Console.WriteLine( $"REQUESTED URL: { ethosResponse.RequestedUrl }" );
                Console.WriteLine( $"Number of resources returned: { ethosResponse.GetContentCount() }" );
            }
        }
        catch ( Exception e )
        {
            Console.WriteLine( e.Message );
        }
    }

    private static async Task GetPagesFromOffsetUsingCriteriaFilterValuesAsync()
    {
        Console.WriteLine( "******* GetPagesFromOffsetWithCriteriaFilterAsync() using criteria filter *******" );
        string resource = "persons";
        string version = "application/vnd.hedtech.integration.v12.3.0+json";
        string criteriaSetName = "names";
        string criteriaKey = "firstName";
        string criteriaValue = "John";
        int pageSize = 50;
        int offset = 40;

        try
        {
            var criteriaFilter = new CriteriaFilter().WithArray( criteriaSetName, (criteriaKey, criteriaValue) );
            List<EthosResponse> ethosResponseList = await filterClient.GetPagesFromOffsetWithCriteriaFilterAsync( resource, version, criteriaFilter, pageSize, offset ) as List<EthosResponse>;
            Console.WriteLine( $"Number of pages returned: {ethosResponseList.Count}" );
            Console.WriteLine( $"OFFSET: { offset }" );
            foreach ( EthosResponse ethosResponse in ethosResponseList )
            {
                Console.WriteLine( $"REQUESTED URL: { ethosResponse.RequestedUrl }" );
                Console.WriteLine( $"Number of resources returned: { ethosResponse.GetContentCount() }" );
            }
        }
        catch ( Exception e )
        {
            Console.WriteLine( e.Message );
        }
    }

    private static async Task GetPagesUsingFilterMapValuesAsync()
    {
        Console.WriteLine( "******* GetPagesWithFilterMapAsync() *******" );
        string resource = "persons";
        string version = "application/vnd.hedtech.integration.v6+json";
        string filterMapKey = "firstName";
        string filterMapValue = "John";
        int pageSize = 50;

        try
        {
            FilterMap filterMap = new FilterMap()
                                      .WithParameterPair( filterMapKey, filterMapValue )
                                      .Build();
            List<EthosResponse> ethosResponseList = await filterClient.GetPagesWithFilterMapAsync( resource, version, filterMap, pageSize ) as List<EthosResponse>;
            Console.WriteLine( $"Number of pages returned: {ethosResponseList.Count}" );
            foreach ( EthosResponse ethosResponse in ethosResponseList )
            {
                Console.WriteLine( $"REQUESTED URL: { ethosResponse.RequestedUrl }" );
                Console.WriteLine( $"Number of resources returned: { ethosResponse.GetContentCount() }" );
            }
        }
        catch ( Exception e )
        {
            Console.WriteLine( e.Message );
        }
    }

    /// <summary>
    /// //Creates ?accountSpecification={"accountingString":"11-01-01-00-11111-11111","amount":"2000","balanceOn":"2018-04-01","submittedBy":"11111111-1111-1111-1111-111111111111"}
    /// </summary>
    /// <returns></returns>
    private static async Task GetPagesUsingNamedQueryAsync()
    {
        Console.WriteLine( "******* GetPagesUsingNamedQueryAsync() *******" );
        string resource = "account-funds-available";
        string version = "application/vnd.hedtech.integration.v8+json";

        string accountingString = "<GL Number>";
        string amount = "2000";
        string balanceOn = "2018-04-01";
        string submittedBy = "11111111-1111-1111-1111-111111111111";


        try
        {
            var namedQuery = new NamedQueryFilter( "accountSpecification" ).WithNamedQuery( ("accountingString", accountingString), ("amount", amount), ("balanceOn", balanceOn), ("submittedBy", submittedBy) ).BuildNamedQuery();
            var ethosResponse = await filterClient.GetWithCriteriaFilterAsync( resource, version, namedQuery );
            Console.WriteLine( $"REQUESTED URL: { ethosResponse.RequestedUrl }" );
            Console.WriteLine( $"Number of resources returned: { ethosResponse.GetContentCount() }" );
        }
        catch ( Exception e )
        {
            Console.WriteLine( e.Message );
        }
    }

    #endregion

    #region Helper Method

    private static void Print( string exampleNumber, string filter )
    {
        Console.WriteLine( $"{exampleNumber}\t: {filter}" );
    }

    #endregion
}
/*
 * ******************************************************************************
 *   Copyright  2022 Ellucian Company L.P. and its affiliates.
 * ******************************************************************************
 */

using Ellucian.Ethos.Integration.Client;
using Ellucian.Ethos.Integration.Client.Extensions;
using Ellucian.Ethos.Integration.Client.Filter.Extensions;
using Ellucian.Ethos.Integration.Client.Proxy.Filter;
using Ellucian.Generated.BpApi.PersonCommentsV100PostRequest;
using Ellucian.Generated.BpApi.PersonCommentsV100PostResponse;
using Ellucian.Generated.BpApi.PersonCommentsV100PutRequest;
using Ellucian.Generated.BpApi.PersonCommentsV100PutResponse;
using Ellucian.Generated.BpApi.PersonSearchV100GetRequest;
using Ellucian.Generated.BpApi.TermCodesV100GetRequest;
using Ellucian.Generated.BpApi.TermCodesV100GetResponse;

using Newtonsoft.Json;

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
        await ExampleFullCrudWithStronglyTypedBpAPiAsync();
        await DoPostQapiEEDMExampleAsync();
        await DoPostQapiBpApiExampleAsync();
    }

    #region All Examples
    /// <summary>
    /// Various Examples of how criteria filter can be built.
    /// </summary>
    /// <returns></returns>
    private static void CriteriaExamples()
    {
        /**********************************************************************************************************
        /* Examples 1 - 17 show how to build criteria filters primarily when making GET requests for Ethos APIs,
        /* as opposed to BPAPIs.  However, the Ethos Integration SDK makes no distinction between Ethos API and
        /* BPAPI requests.  How to build a criteria filter largely depends on which Ethos API a request is made for.
        /*********************************************************************************************************/

        Print("Examples 1 - 17 are primarily used for Ethos API requests as opposed to BPAPI requests, " +
              "though the Ethos Integration SDK makes no distinction between the type of API request. ", "");

        //?criteria={"lastName":"Smith"}
        var filter = new CriteriaFilter().WithSimpleCriteria("lastName", "Smith").BuildCriteria();
        Print("Criteria1", filter);

        //?criteria={"names":[{"firstName":"John","lastName":"smith"}]}
        filter = new CriteriaFilter().WithArray("names", new CriteriaFilter().WithSimpleCriteria("firstName", "John").WithSimpleCriteria("lastName", "smith")).BuildCriteria();
        Print("Criteria2", filter);

        //?criteria={"someName":{"anotherName":{"names":{"lastName":"Smith"}}}}
        filter = new CriteriaFilter().WithSimpleCriteria("names", ("lastName", "Smith")).WithSimpleCriteria("anotherName").WithSimpleCriteria("someName").BuildCriteria();
        Print("Criteria3-a", filter);

        //?criteria={"someName":{"anotherName":{"names":{"lastName":"Smith"}}}}
        filter = new CriteriaFilter().WithSimpleCriteria("someName", new CriteriaFilter().WithSimpleCriteria("anotherName", new CriteriaFilter().WithSimpleCriteria("names", ("lastName", "Smith")))).BuildCriteria();
        Print("Criteria3-b", filter);

        //?criteria={"statuses":["active","approved"]}
        filter = new CriteriaFilter().WithArray("statuses", new[] { "active", "approved" }).BuildCriteria();
        Print("Criteria4", filter);

        //?criteria={"academicLevels":[{"id":"11111111-1111-1111-1111-111111111111"},{"id":"11111111-1111-1111-1111-111111111112"}]}
        filter = new CriteriaFilter().WithArray("academicLevels", ("id", "11111111-1111-1111-1111-111111111111"), ("id", "11111111-1111-1111-1111-111111111112")).BuildCriteria();
        Print("Criteria5", filter);

        //?criteria={"authors":[{"person":{"id":"11111111-1111-1111-1111-111111111111"}}]}
        filter = new CriteriaFilter().WithArray("authors", new CriteriaFilter().WithSimpleCriteria("person", ("id", "11111111-1111-1111-1111-111111111111"))).BuildCriteria();
        Print("Criteria6", filter);

        //?criteria={"names":{"personalNames":[{"title":"Mr."},{"title":"Mr"}]}}
        filter = new CriteriaFilter().WithSimpleCriteria("names", new CriteriaFilter().WithArray("personalNames", ("title", "Mr."), ("title", "Mr"))).BuildCriteria();
        Print("Criteria7", filter);

        //?criteria={"credentials":[{"type":{"id":"11111111-1111-1111-1111-111111111111"}},{"value":"bannerId"}]}
        filter = new CriteriaFilter().WithArray("credentials",
                     new CriteriaFilter().WithSimpleCriteria("type", ("id", "11111111-1111-1111-1111-111111111111")),
                     new CriteriaFilter().WithSimpleCriteria("value", "bannerId")).BuildCriteria();
        Print("Criteria8", filter);

        //?criteria={"academicLevels":[{"id":"11111111-1111-1111-1111-111111111111","firstName":"John"},{"id":"11111111-1111-1111-1111-111111111112","lastName":"Smith"}]}
        Dictionary<string, object> dict = new Dictionary<string, object>();
        dict.Add("id", "11111111-1111-1111-1111-111111111111");
        dict.Add("firstName", "John");
        Dictionary<string, object> dict1 = new Dictionary<string, object>();
        dict1.Add("id", "11111111-1111-1111-1111-111111111112");
        dict1.Add("lastName", "Smith");
        filter = new CriteriaFilter().WithArray("academicLevels", dict, dict1).BuildCriteria();
        Print("Criteria9", filter);

        //?criteria={"solicitors":[{"solicitor":{"constituent":{"person":{"id":"11111111-1111-1111-1111-111111111111"}}}},{"solicitor":{"constituent":{"person":{"id":"11111111-1111-1111-1111-111111111111"}}}}]}
        var solicitors = new CriteriaFilter().WithSimpleCriteria("constituent", new CriteriaFilter().WithSimpleCriteria("person", ("id", "11111111-1111-1111-1111-111111111111")))
                            .WithSimpleCriteria("solicitor");
        filter = new CriteriaFilter().WithArray("solicitors", solicitors, solicitors).BuildCriteria();
        Print("Criteria10", filter);
        //Or ?criteria={"solicitors":[{"solicitor":{"constituent":{"person":{"id":"11111111-1111-1111-1111-111111111111"}}}}]}
        filter = new CriteriaFilter().WithArray("solicitors",
                    new CriteriaFilter().WithSimpleCriteria("solicitor",
                        new CriteriaFilter().WithSimpleCriteria("constituent",
                            new CriteriaFilter().WithSimpleCriteria("person", ("id", "11111111-1111-1111-1111-111111111111"))))).BuildCriteria();
        Print("Criterias11", filter);

        //?criteria={"person":[{"names":{"personalNames":{"firstName":"John"}}},{"names":{"personalNames":{"firstName":"Johny"}}}]}
        var myName = new CriteriaFilter().WithSimpleCriteria("names", new CriteriaFilter().WithSimpleCriteria("personalNames", ("firstName", "John")));
        var myName2 = new CriteriaFilter().WithSimpleCriteria("names", new CriteriaFilter().WithSimpleCriteria("personalNames", ("firstName", "Johny")));
        filter = new CriteriaFilter().WithArray("person", myName, myName2).BuildCriteria();
        Print("Criteria12", filter);

        //?criteria={"startOn":{"$eq":"2019-04-22T10:03:07+00:00"}}
        filter = new CriteriaFilter().WithSimpleCriteria("startOn", ("$eq", "2019-04-22T10:03:07+00:00")).BuildCriteria();
        Print("Criteria13", filter);

        //?criteria={"ethos":{"resources":["persons","organizations"]}}
        filter = new CriteriaFilter().WithSimpleCriteria("resources", new[] { "persons", "organizations" }).WithSimpleCriteria("ethos").BuildCriteria();
        Print("Criteria14", filter);

        //?criteria={"credentials":[{"type":"bannerId","value":"SomeUser"}]}
        filter = new CriteriaFilter().WithArray("credentials", new CriteriaFilter().WithSimpleCriteria("type", "bannerId").WithSimpleCriteria("value", "SomeUser")).BuildCriteria();
        Print("Criteria15", filter);

        //?criteria={"startOn":{"year":"2021","month":"April"}}
        filter = new CriteriaFilter().WithSimpleCriteria("startOn", new CriteriaFilter().WithSimpleCriteria("year", "2021").WithSimpleCriteria("month", "April")).BuildCriteria();
        Print("Criteria16", filter);

        //?criteria={"ethos":{"resources":["persons","organizations"]}} 
        filter = new CriteriaFilter().WithSimpleCriteria("resources", new[] { "persons", "organizations" }).WithSimpleCriteria("ethos").BuildCriteria();
        Print("Criteria17", filter);

        /**********************************************************************************************************
        /* Examples 18 - 19 show how to build criteria filters primarily when making GET requests for BPAPIs,
        /* as opposed to Ethos APIs.  However, the Ethos Integration SDK makes no distinction between Ethos API and
        /* BPAPI requests.
        /*********************************************************************************************************/

        Print("Examples 18 - 19 are primarily used for BPAPI requests as opposed to Ethos API requests, " + 
              "though the Ethos Integration SDK makes no distinction between the type of API request.", "");

        //?criteria={"statusInd":"A","acctCode":"04"}
        filter = new CriteriaFilter().WithSimpleCriteria("statusInd", "A").WithSimpleCriteria("acctCode", "04").BuildCriteria();
        Print("Criteria18", filter);

        //?statusInd=A&acctCode=04
        FilterMap filterMap = new FilterMap().WithParameterPair("statusInd", "A").WithParameterPair("acctCode", "04").Build();
        Print("Criteria19", filterMap.ToString());
    }

    /// <summary>
    /// Various examples of how named query is built.
    /// </summary>
    private static void NamedQueryExamples()
    {
        //?registrationStatusesByAcademicPeriod={"statuses":[{"detail":{"id":"11111111-1111-1111-1111-111111111111"}},{"detail":{"id":"11111111-1111-1111-1111-111111111112"}}],"academicPeriod":{"id":"11111111-1111-1111-1111-111111111113"}}
        var filter = new NamedQueryFilter("registrationStatusesByAcademicPeriod")
                        .WithNamedQuery("statuses", ("detail", "id", "11111111-1111-1111-1111-111111111111"), ("detail", "id", "11111111-1111-1111-1111-111111111112"))
                        .WithNamedQuery("academicPeriod", "id", "11111111-1111-1111-1111-111111111113")
                        .BuildNamedQuery();
        Print("NamedQuery1", filter);

        //?instructor={"instructor":{"id":"11111111-1111-1111-1111-111111111111"}}
        filter = new NamedQueryFilter("instructor")
                    .WithNamedQuery("instructor", "id", "11111111-1111-1111-1111-111111111111")
                    .BuildNamedQuery();
        Print("NamedQuery2", filter);

        //?keywordSearch={"keywordSearch":"Origins"}
        filter = new NamedQueryFilter("keywordSearch")
                    .WithNamedQuery("keywordSearch", "Origins")
                    .BuildNamedQuery();
        Print("NamedQuery3", filter);
    }

    /// <summary>
    /// Creates : ?criteria={"names":[{"firstName":"John","lastName":"smith"}]}
    /// </summary>
    /// <returns></returns>
    private static async Task GetNamesUsingCriteriaFilterStringAsync()
    {
        Console.WriteLine("******* GetNamesUsingCriteriaFilterStringAsync() using filter string *******");
        string resource = "persons";
        string version = "application/vnd.hedtech.integration.v12.3.0+json";
        string criteriaFilterStr = "?criteria={\"names\":[{\"firstName\":\"John\",\"lastName\":\"Smith\"}]}";

        Console.WriteLine($"Criteria: { criteriaFilterStr }");
        try
        {
            EthosResponse ethosResponse = await filterClient.GetWithCriteriaFilterAsync(resource, version, criteriaFilterStr);
            Console.WriteLine($"REQUESTED URL: { ethosResponse.RequestedUrl }");
            Console.WriteLine($"Number of resources returned: { ethosResponse.GetContentCount() }\r\n");
            Console.WriteLine(ethosResponse.GetContentAsJson().ToString());
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    /// <summary>
    /// Creates : ?criteria={"roles":[{"role":"instructor"},{"role":"advisor"}]}
    /// </summary>
    /// <returns></returns>
    private static async Task GetUsingCriteriaFilterRolesAsync()
    {
        Console.WriteLine("******* GetUsingCriteriaFilterRolesAsync() using CriteriaFilter *******");
        string resource = "persons";
        string version = "application/vnd.hedtech.integration.v12.3.0+json";
        string criteriaFilterStr = new CriteriaFilter()
                                            .WithArray("roles", ("role", "instructor"), ("role", "advisor"))
                                            .BuildCriteria();
        Console.WriteLine($"Criteria: { criteriaFilterStr }");

        try
        {
            EthosResponse ethosResponse = await filterClient.GetWithCriteriaFilterAsync(resource, version, criteriaFilterStr);
            Console.WriteLine($"REQUESTED URL: { ethosResponse.RequestedUrl }");
            Console.WriteLine($"Number of resources returned: { ethosResponse.GetContentCount() }\r\n");
            Console.WriteLine(ethosResponse.GetContentAsJson().ToString());
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    /// <summary>
    /// Creates: ?criteria={"credentials":[{"type":"bannerSourcedId","value":"684"}]}
    /// </summary>
    /// <returns></returns>
    private static async Task GetUsingCriteriaFilterCredentialsAsync()
    {
        Console.WriteLine("******* GetUsingCriteriaFilterCredentialsAsync() using values *******");
        string resource = "persons";
        string version = "application/vnd.hedtech.integration.v12.3.0+json";
        string criteriaFilterStr = new CriteriaFilter()
                                            .WithArray("roles", ("role", "instructor"), ("role", "advisor"))
                                            .BuildCriteria();
        Console.WriteLine($"Criteria: { criteriaFilterStr }");

        try
        {
            EthosResponse ethosResponse = await filterClient.GetWithCriteriaFilterAsync(resource, version, criteriaFilterStr);
            Console.WriteLine($"REQUESTED URL: { ethosResponse.RequestedUrl }");
            Console.WriteLine($"Number of resources returned: { ethosResponse.GetContentCount() }\r\n");
            Console.WriteLine(ethosResponse.GetContentAsJson().ToString());
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    /// <summary>
    /// Creates: ?criteria={"alternativeCredentials":[{"type":{"id":"11111111-1111-1111-1111-111111111111"},"value":"abcd"}]}
    /// </summary>
    /// <returns></returns>
    private static async Task GetUsingCriteriaFilterAlternateCredentialsAsync()
    {
        Console.WriteLine("******* GetUsingCriteriaFilterAlternateCredentialsAsync() using values *******");
        string resource = "persons";
        string version = "application/vnd.hedtech.integration.v12.3.0+json";
        string id = "11111111-1111-1111-1111-111111111111";
        string value = "abcd";
        string criteriaFilterStr = new CriteriaFilter().WithArray("alternativeCredentials",
                                   new CriteriaFilter().WithSimpleCriteria("type", new CriteriaFilter().WithSimpleCriteria("id", id)).WithSimpleCriteria("value", value))
                                   .BuildCriteria();
        Console.WriteLine($"Criteria: { criteriaFilterStr }");

        try
        {
            EthosResponse ethosResponse = await filterClient.GetWithCriteriaFilterAsync(resource, version, criteriaFilterStr);
            Console.WriteLine($"REQUESTED URL: { ethosResponse.RequestedUrl }");
            Console.WriteLine($"Number of resources returned: { ethosResponse.GetContentCount() }\r\n");
            Console.WriteLine(ethosResponse.GetContentAsJson().ToString());
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    /// <summary>
    /// Creates: ?criteria={"emails":[{"address":"abcd@xyz.com"},{"address":"testing123@testing.com"}]}
    /// </summary>
    /// <returns></returns>
    private static async Task GetUsingCriteriaFilterEmailsAsync()
    {
        Console.WriteLine("******* GetUsingCriteriaFilterEmailsAsync() using values *******");
        string resource = "persons";
        string version = "application/vnd.hedtech.integration.v12.3.0+json";
        string email1 = "abcd@xyz.com";
        string email2 = "testing123@testing.com";
        string criteriaFilterStr = new CriteriaFilter()
                                    .WithArray("emails", ("address", email1), ("address", email2))
                                    .BuildCriteria();
        Console.WriteLine($"Criteria: { criteriaFilterStr }");

        try
        {
            EthosResponse ethosResponse = await filterClient.GetWithCriteriaFilterAsync(resource, version, criteriaFilterStr);
            Console.WriteLine($"REQUESTED URL: { ethosResponse.RequestedUrl }");
            Console.WriteLine($"Number of resources returned: { ethosResponse.GetContentCount() }\r\n");
            Console.WriteLine(ethosResponse.GetContentAsJson().ToString());
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    /// <summary>
    /// Creates: ?criteria={"decidedOn":{"$lte":"2003-03-22T18:57:25Z"}}
    /// </summary>
    /// <returns></returns>
    private static async Task GetUsingCriteriaFilterAdmissionDecisionsDecidedOnAsync()
    {
        Console.WriteLine("******* GetUsingCriteriaFilterAdmissionDecisionsDecidedOnAsync() using values *******");
        string resource = "admission-decisions";
        string decidedOn = "2003-03-22T18:57:25Z";
        string criteriaFilterStr = new CriteriaFilter()
                                    .WithSimpleCriteria("decidedOn", ("$lte", decidedOn))
                                    .BuildCriteria();
        Console.WriteLine($"Criteria: { criteriaFilterStr }");

        try
        {
            EthosResponse ethosResponse = await filterClient.GetWithCriteriaFilterAsync(resource, criteriaFilterStr);
            Console.WriteLine($"REQUESTED URL: { ethosResponse.RequestedUrl }");
            Console.WriteLine($"Number of resources returned: { ethosResponse.GetContentCount() }\r\n");
            Console.WriteLine(ethosResponse.GetContentAsJson().ToString());
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    /// <summary>
    /// Creates: ?criteria={"instructionalMethodDetails":[{"instructionalMethod":{"id":"11111111-1111-1111-1111-111111111111"}},{"instructionalMethod":{"id":"11111111-1111-1111-1111-111111111112"}}]}
    /// </summary>
    /// <returns></returns>
    private static async Task GetUsingCriteriaFilterCoursesInstructionalMethodDetailsAsync()
    {
        Console.WriteLine("******* GetUsingCriteriaFilterCoursesInstructionalMethodDetailsAsync() using values *******");
        string resource = "courses";
        var id1 = "11111111-1111-1111-1111-111111111111";
        var id2 = "11111111-1111-1111-1111-111111111112";
        string criteriaFilterStr = new CriteriaFilter()
                                    .WithArray("instructionalMethodDetails",
                                    new CriteriaFilter().WithSimpleCriteria("instructionalMethod", ("id", id1)),
                                    new CriteriaFilter().WithSimpleCriteria("instructionalMethod", ("id", id2)))
                                    .BuildCriteria();
        Console.WriteLine($"Criteria: { criteriaFilterStr }");

        try
        {
            EthosResponse ethosResponse = await filterClient.GetWithCriteriaFilterAsync(resource, criteriaFilterStr);
            Console.WriteLine($"REQUESTED URL: { ethosResponse.RequestedUrl }");
            Console.WriteLine($"Number of resources returned: { ethosResponse.GetContentCount() }\r\n");
            Console.WriteLine(ethosResponse.GetContentAsJson().ToString());
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private static async Task GetUsingFilterMapAsync()
    {
        Console.WriteLine("******* GetWithFilterMapAsync() using filterMap *******");
        string resource = "persons";
        string version = "application/vnd.hedtech.integration.v6+json";
        string filterKey = "firstName";
        string filterValue = "John";

        try
        {
            FilterMap filterMap = new FilterMap()
                                      .WithParameterPair(filterKey, filterValue)
                                      .WithParameterPair("lastName", "Smith")
                                      .Build();
            EthosResponse ethosResponse = await filterClient.GetWithFilterMapAsync(resource, version, filterMap);
            Console.WriteLine($"REQUESTED URL: { ethosResponse.RequestedUrl }");
            Console.WriteLine($"Number of resources returned: { ethosResponse.GetContentCount() }\r\n");
            Console.WriteLine(ethosResponse.GetContentAsJson().ToString());
        }

        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    private static async Task GetPagesUsingCriteriaFilterValuesAsync()
    {
        Console.WriteLine("******* GetPagesWithCriteriaFilterAsync() using criteria filter *******");
        string resource = "persons";
        string version = "application/vnd.hedtech.integration.v12.3.0+json";
        string criteriaSetName = "names";
        string criteriaKey = "firstName";
        string criteriaValue = "John";
        int pageSize = 50;

        try
        {
            var criteriaFilter = new CriteriaFilter().WithArray(criteriaSetName, (criteriaKey, criteriaValue));
            Console.WriteLine(criteriaFilter.BuildCriteria());
            List<EthosResponse>? ethosResponseList = await filterClient.GetPagesWithCriteriaFilterAsync(resource, version, criteriaFilter, pageSize) as List<EthosResponse>;
            Console.WriteLine($"Number of pages returned: {ethosResponseList?.Count}");
            foreach (EthosResponse ethosResponse in ethosResponseList!)
            {
                Console.WriteLine($"REQUESTED URL: { ethosResponse.RequestedUrl }");
                Console.WriteLine($"Number of resources returned: { ethosResponse.GetContentCount() }");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    private static async Task GetPagesFromOffsetUsingCriteriaFilterValuesAsync()
    {
        Console.WriteLine("******* GetPagesFromOffsetWithCriteriaFilterAsync() using criteria filter *******");
        string resource = "persons";
        string version = "application/vnd.hedtech.integration.v12.3.0+json";
        string criteriaSetName = "names";
        string criteriaKey = "firstName";
        string criteriaValue = "John";
        int pageSize = 50;
        int offset = 40;

        try
        {
            var criteriaFilter = new CriteriaFilter().WithArray(criteriaSetName, (criteriaKey, criteriaValue));
            List<EthosResponse>? ethosResponseList = await filterClient.GetPagesFromOffsetWithCriteriaFilterAsync(resource, version, criteriaFilter, pageSize, offset) as List<EthosResponse>;
            Console.WriteLine($"Number of pages returned: {ethosResponseList?.Count}");
            Console.WriteLine($"OFFSET: { offset }");
            foreach (EthosResponse ethosResponse in ethosResponseList!)
            {
                Console.WriteLine($"REQUESTED URL: { ethosResponse.RequestedUrl }");
                Console.WriteLine($"Number of resources returned: { ethosResponse.GetContentCount() }");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    private static async Task GetPagesUsingFilterMapValuesAsync()
    {
        Console.WriteLine("******* GetPagesWithFilterMapAsync() *******");
        string resource = "persons";
        string version = "application/vnd.hedtech.integration.v6+json";
        string filterMapKey = "firstName";
        string filterMapValue = "John";
        int pageSize = 50;

        try
        {
            FilterMap filterMap = new FilterMap()
                                      .WithParameterPair(filterMapKey, filterMapValue)
                                      .Build();
            List<EthosResponse>? ethosResponseList = await filterClient.GetPagesWithFilterMapAsync(resource, version, filterMap, pageSize) as List<EthosResponse>;
            Console.WriteLine($"Number of pages returned: {ethosResponseList?.Count}");
            foreach (EthosResponse ethosResponse in ethosResponseList!)
            {
                Console.WriteLine($"REQUESTED URL: { ethosResponse.RequestedUrl }");
                Console.WriteLine($"Number of resources returned: { ethosResponse.GetContentCount() }");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    /// <summary>
    /// //Creates ?accountSpecification={"accountingString":"11-01-01-00-11111-11111","amount":"2000","balanceOn":"2018-04-01","submittedBy":"11111111-1111-1111-1111-111111111111"}
    /// </summary>
    /// <returns></returns>
    private static async Task GetPagesUsingNamedQueryAsync()
    {
        Console.WriteLine("******* GetPagesUsingNamedQueryAsync() *******");
        string resource = "account-funds-available";
        string version = "application/vnd.hedtech.integration.v8+json";

        string accountingString = "<GL Number>";
        string amount = "2000";
        string balanceOn = "2018-04-01";
        string submittedBy = "11111111-1111-1111-1111-111111111111";


        try
        {
            var namedQuery = new NamedQueryFilter("accountSpecification").WithNamedQuery(("accountingString", accountingString), ("amount", amount), ("balanceOn", balanceOn), ("submittedBy", submittedBy)).BuildNamedQuery();
            var ethosResponse = await filterClient.GetWithCriteriaFilterAsync(resource, version, namedQuery);
            Console.WriteLine($"REQUESTED URL: { ethosResponse.RequestedUrl }");
            Console.WriteLine($"Number of resources returned: { ethosResponse.GetContentCount() }");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    /// <summary>
    /// This example illustrate hot to use strongly typed object to perform a GET/POST/PUT operation.
    /// </summary>
    /// <returns></returns>
    private static async Task ExampleFullCrudWithStronglyTypedBpAPiAsync()
    {
        BuildEthosFilterClient();
        var resourceName = "person-search";
        var filterMap = new FilterMap() { };
        var filter = filterMap.WithParameterPair("lastName", "abbe").Build();

        //Use filter client to perform a serch using http GET to find records.
        var response = await filterClient.GetWithFilterMapAsync<IEnumerable<PersonSearchV100GetRequest>>(resourceName, filter);
        int num = GetRandomNumber(((IEnumerable<PersonSearchV100GetRequest>)response.Dto).Count());

        try
        {
            //Select a random record from searched records collection.
            PersonSearchV100GetRequest? searchedRecord = ((IEnumerable<PersonSearchV100GetRequest>)response.Dto).ToList()[num];
            Console.WriteLine($"{ searchedRecord.LastName } { searchedRecord.Id }");

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
            EthosResponse ethosPostResponse = await filterClient.PostAsync<PersonCommentsV100PostRequest>("person-comments", pcPostReq);
            var pcResps = ethosPostResponse.Deserialize<IEnumerable<PersonCommentsV100PostResponse>>();
            foreach (PersonCommentsV100PostResponse pcResp in pcResps)
            {
                Console.WriteLine($"Contact Date: {pcResp.ContactDate}, Text: {pcResp.Text}, Text Nar: {pcResp.TextNar}");
            }

            //Create a strongly typed object for PUT.
            PersonCommentsV100PutRequest putReq = new PersonCommentsV100PutRequest()
            {
                Id = pcPostReq.Id,
                CmttCode = pcPostReq.CmttCode,
                ConfidentialInd = pcPostReq.ConfidentialInd,
                ContactDate = DateTime.Now.AddDays(1),
                OrigCode = pcPostReq.OrigCode,
                Text = pcPostReq.Text,
                TextNar = pcPostReq.TextNar
            };

            //Perform PUT
            EthosResponse ethosPutResponse = await filterClient.PutAsync<PersonCommentsV100PutRequest>("person-comments", putReq);
            var putResps = ethosPutResponse.Deserialize<IEnumerable<PersonCommentsV100PutResponse>>();
            foreach (PersonCommentsV100PutResponse putResp in putResps)
            {
                Console.WriteLine($"Contact Date: {putResp.ContactDate}, Text: {putResp.Text}, Text Nar: {putResp.TextNar}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    /// <summary>
    /// How to call QAPI EEDM end point using POST.
    /// </summary>
    /// <returns></returns>
    private static async Task DoPostQapiEEDMExampleAsync()
    {
        string resource = "persons";
        string version = "application/vnd.hedtech.integration.v12+json";
        string requestBody = GetPersonRequestBody();
        try
        {
            var ethosResponses = await filterClient.PostQapiAsync( resource, version, requestBody );
            Console.WriteLine( $"Total records retrieved: {ethosResponses.GetContentCount()}." );
            Console.WriteLine( $"Json content: {ethosResponses.Content}" );
        }
        catch ( Exception e )
        {
            Console.WriteLine( e.Message );
        }
    }

    /// <summary>
    /// How to call QAPI BPAPI end point using POST.
    /// </summary>
    /// <returns></returns>
    private static async Task DoPostQapiBpApiExampleAsync()
    {
        string resource = "term-codes";
        string version = "application/vnd.hedtech.integration.v1.0.0+json";
        TermCodesV100GetRequest requestBody = GetTermCodesRequestBody();
        try
        {
            var ethosResponses = await filterClient.PostQapiAsync<TermCodesV100GetRequest>( resource, requestBody, version );
            Console.WriteLine( $"Total records retrieved: {ethosResponses.GetContentCount()}." );
            Console.WriteLine( $"Json content: {ethosResponses.Content}" );
        }
        catch ( Exception e )
        {
            Console.WriteLine( e.Message );
        }
    }

    /// <summary>
    /// Persons requests body.
    /// </summary>
    /// <returns></returns>
    private static string GetPersonRequestBody()
    {
        return @"{'names':[{'firstName':'Robin','fullName':'Robin Hawk','lastName':'Hawk','preference':'preferred','title':'Ms.'}]}";
    }

    private static TermCodesV100GetRequest GetTermCodesRequestBody()
    {
        return new TermCodesV100GetRequest()
        {
            AcyrCode = "2017",
            FaEndPeriod = 12,
            FaPeriod = 1
        };
    }

    #endregion

    #region Helper Method

    private static void Print(string exampleNumber, string filter)
    {
        Console.WriteLine($"{exampleNumber}\t: {filter}");
    }

    #endregion
}

/*
 * ******************************************************************************
 *   Copyright  2022 Ellucian Company L.P. and its affiliates.
 * ******************************************************************************
 */

using Ellucian.Ethos.Integration.Client;
using Ellucian.Generated.Eedm;

namespace Ellucian.Examples;

public class EEDMExample : ExampleBase
{

    public static async Task RunEedmExamples()
    {
        client = GetEthosProxyClient();
        await ExampleGetStudentCohortAsync();
        await ExampleFullCrudWithStronglyTypedAsync();
        //ExamplesCriteria();
        //ExamplesNamedQuery();
    }

    /// <summary>
    /// GET example for student-cohorts
    /// </summary>
    /// <returns></returns>
    private static async Task ExampleGetStudentCohortAsync()
    {
        var response = await client.GetAsync<IEnumerable<StudentCohorts_V7_2_0>>( "student-cohorts" );

        if ( response != null )
        {
            Console.WriteLine( "" );
            Console.WriteLine( string.Format( "{0, -40} {1, -20}{2, -11}{3, -40}{4, -60}", "ID", "COHORT-TYPE", "CODE", "TITLE", "DESCRIPTION" ) );
            foreach ( StudentCohorts_V7_2_0 item in response.Dto )
            {
                Console.WriteLine( string.Format( "{0, -40} {1, -20}{2, -11}{3, -40}{4, -60}", item.Id, item.StudentCohortType.ToString().Trim(), item.Code.Trim(), item.Title.Trim(), item.Description ?? "No Description") );
            }
        }
    }

    private static async Task ExampleFullCrudWithStronglyTypedAsync()
    {
        try
        {
            Persons_V12_3_0? person = await GetPersonAsync();
            string? personId = person?.Id.ToString();

            // Populate person holds record.
            PersonHolds_V6_0 personHold1 = new PersonHolds_V6_0
            {
                Id = "00000000-0000-0000-0000-000000000000",
                StartOn = DateTimeOffset.Now,
                Person = new GuidObject2 { Id = personId! },
                PersonHoldTypeType = new PersonHoldTypeType() { PersonHoldCategory = PersonHoldCategoryTypes.Financial }
            };

            // POST
            EthosResponse response = await client.PostAsync<PersonHolds_V6_0>( "person-holds", personHold1 );

            // PRINT
            var dto = response.Dto as PersonHolds_V6_0;
            Console.WriteLine( "Created a 'person-holds' record:" );
            Console.WriteLine( dto!.Id );
            Console.WriteLine();

            string newId = response.Dto.Id.ToString();
            DateTimeOffset newHoldEnd = DateTimeOffset.Now.AddDays( 1 );
            personHold1.StartOn = newHoldEnd;
            // PUT
            response = await client.PutAsync<PersonHolds_V6_0>( "person-holds", dto, newId );
            Console.WriteLine( $"Successfully updated person-holds record {newId}" );

            // DELETE
            await client.DeleteAsync( "person-holds", newId );
            Console.WriteLine( $"Successfully deleted person-holds record {newId}" );

            // attempt to get the formerly created, now deleted, record, and make sure it is no longer there.
            try
            {
                await client.GetByIdAsync( "person-holds", newId );
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

    ///// <summary>
    ///// Various Examples of how criteria filter can be built.
    ///// </summary>
    ///// <returns></returns>
    //private static void ExamplesCriteria()
    //{
    //    //?criteria={"lastName":"Smith"}
    //    var filter = new CriteriaFilter().WithSimpleCriteria( "lastName", "Smith" ).BuildCriteria();
    //    Print( "Criteria1", filter );

    //    //?criteria={"names":[{"firstName":"John","lastName":"smith"}]}
    //    filter = new CriteriaFilter().WithArray( "names", new CriteriaFilter().WithSimpleCriteria( "firstName", "John" ).WithSimpleCriteria( "lastName", "smith" ) ).BuildCriteria();
    //    Print( "Criteria2_4", filter );

    //    //?criteria={"someName":{"anotherName":{"names":{"lastName":"Smith"}}}}
    //    filter = new CriteriaFilter().WithSimpleCriteria( "names", ("lastName", "Smith") ).WithSimpleCriteria( "anotherName" ).WithSimpleCriteria( "someName" ).BuildCriteria();
    //    Print( "Criteria3-a", filter );

    //    //?criteria={"someName":{"anotherName":{"names":{"lastName":"Smith"}}}}
    //    filter = new CriteriaFilter().WithSimpleCriteria( "someName", new CriteriaFilter().WithSimpleCriteria( "anotherName", new CriteriaFilter().WithSimpleCriteria( "names", ("lastName", "Smith") ) ) ).BuildCriteria();
    //    Print( "Criteria3-b", filter );

    //    //?criteria={"statuses":["active","approved"]}
    //    filter = new CriteriaFilter().WithArray( "statuses", new [] { "active", "approved" } ).BuildCriteria();
    //    Print( "Criteria5", filter );

    //    //?criteria={"academicLevels":[{"id":"11111111-1111-1111-1111-111111111111"},{"id":"11111111-1111-1111-1111-111111111112"}]}
    //    filter = new CriteriaFilter().WithArray( "academicLevels", ("id", "11111111-1111-1111-1111-111111111111"), ("id", "11111111-1111-1111-1111-111111111112") ).BuildCriteria();
    //    Print( "Criteria6", filter );

    //    //?criteria={"authors":[{"person":{"id":"11111111-1111-1111-1111-111111111111"}}]}
    //    filter = new CriteriaFilter().WithArray( "authors", new CriteriaFilter().WithSimpleCriteria( "person", ("id", "11111111-1111-1111-1111-111111111111") ) ).BuildCriteria();
    //    Print( "Criteria7", filter );

    //    //?criteria={"names":{"personalNames":[{"title":"Mr."},{"title":"Mr"}]}}
    //    filter = new CriteriaFilter().WithSimpleCriteria( "names", new CriteriaFilter().WithArray( "personalNames", ("title", "Mr."), ("title", "Mr") ) ).BuildCriteria();
    //    Print( "Criteria8", filter );

    //    //?criteria={"credentials":[{"type":{"id":"11111111-1111-1111-1111-111111111111"}},{"value":"bannerId"}]}
    //    filter = new CriteriaFilter().WithArray( "credentials",
    //                 new CriteriaFilter().WithSimpleCriteria( "type", ("id", "11111111-1111-1111-1111-111111111111") ),
    //                 new CriteriaFilter().WithSimpleCriteria( "value", "bannerId" ) ).BuildCriteria();
    //    Print( "Criteria9", filter );

    //    //?criteria={"academicLevels":[{"id":"11111111-1111-1111-1111-111111111111","firstName":"John"},{"id":"11111111-1111-1111-1111-111111111112","lastName":"Smith"}]}
    //    Dictionary<string, object> dict = new Dictionary<string, object>();
    //    dict.Add( "id", "11111111-1111-1111-1111-111111111111" );
    //    dict.Add( "firstName", "John" );
    //    Dictionary<string, object> dict1 = new Dictionary<string, object>();
    //    dict1.Add( "id", "11111111-1111-1111-1111-111111111112" );
    //    dict1.Add( "lastName", "Smith" );
    //    filter = new CriteriaFilter().WithArray( "academicLevels", dict, dict1 ).BuildCriteria();
    //    Print( "Criteria10", filter );

    //    //?criteria={"solicitors":[{"solicitor":{"constituent":{"person":{"id":"11111111-1111-1111-1111-111111111111"}}}},{"solicitor":{"constituent":{"person":{"id":"11111111-1111-1111-1111-111111111111"}}}}]}
    //    var solicitors = new CriteriaFilter().WithSimpleCriteria( "constituent", new CriteriaFilter().WithSimpleCriteria( "person", ("id", "11111111-1111-1111-1111-111111111111") ) )
    //                        .WithSimpleCriteria( "solicitor" );
    //    filter = new CriteriaFilter().WithArray( "solicitors", solicitors, solicitors ).BuildCriteria();
    //    Print( "Criteria11", filter );
    //    //Or ?criteria={"solicitors":[{"solicitor":{"constituent":{"person":{"id":"11111111-1111-1111-1111-111111111111"}}}}]}
    //    filter = new CriteriaFilter().WithArray( "solicitors",
    //                new CriteriaFilter().WithSimpleCriteria( "solicitor",
    //                    new CriteriaFilter().WithSimpleCriteria( "constituent",
    //                        new CriteriaFilter().WithSimpleCriteria( "person", ("id", "11111111-1111-1111-1111-111111111111") ) ) ) ).BuildCriteria();
    //    Print( "Criterias11", filter );

    //    //?criteria={"person":[{"names":{"personalNames":{"firstName":"John"}}},{"names":{"personalNames":{"firstName":"Johny"}}}]}
    //    var myName = new CriteriaFilter().WithSimpleCriteria( "names", new CriteriaFilter().WithSimpleCriteria( "personalNames", ("firstName", "John") ) );
    //    var myName2 = new CriteriaFilter().WithSimpleCriteria( "names", new CriteriaFilter().WithSimpleCriteria( "personalNames", ("firstName", "Johny") ) );
    //    filter = new CriteriaFilter().WithArray( "person", myName, myName2 ).BuildCriteria();
    //    Print( "Criteria12", filter );

    //    //?criteria={"startOn":{"$eq":"2019-04-22T10:03:07+00:00"}}
    //    filter = new CriteriaFilter().WithSimpleCriteria( "startOn", ("$eq", "2019-04-22T10:03:07+00:00") ).BuildCriteria();
    //    Print( "Criteria13", filter );

    //    //?criteria={"ethos":{"resources":["persons","organizations"]}}
    //    filter = new CriteriaFilter().WithSimpleCriteria( "resources", new [] { "persons", "organizations" } ).WithSimpleCriteria( "ethos" ).BuildCriteria();
    //    Print( "Criteria14", filter );

    //    //?criteria={"credentials":[{"type":"bannerId","value":"SomeUser"}]}
    //    filter = new CriteriaFilter().WithArray( "credentials", new CriteriaFilter().WithSimpleCriteria( "type", "bannerId" ).WithSimpleCriteria( "value", "SomeUser" ) ).BuildCriteria();
    //    Print( "Criteria15", filter );

    //    //?criteria={"startOn":{"year":"2021","month":"April"}}
    //    filter = new CriteriaFilter().WithSimpleCriteria( "startOn", new CriteriaFilter().WithSimpleCriteria( "year", "2021" ).WithSimpleCriteria( "month", "April" ) ).BuildCriteria();
    //    Print( "Criteria16", filter );


    //    //?criteria={"ethos":{"resources":["persons","organizations"]}} 
    //    filter = new CriteriaFilter().WithSimpleCriteria( "resources", new [] { "persons", "organizations" } ).WithSimpleCriteria( "ethos" ).BuildCriteria();
    //    Print( "Criteria17", filter );
    //}

    ///// <summary>
    ///// Various examples of how named query can be built.
    ///// </summary>
    //private static void ExamplesNamedQuery()
    //{
    //    //?registrationStatusesByAcademicPeriod={"statuses":[{"detail":{"id":"11111111-1111-1111-1111-111111111111"}},{"detail":{"id":"11111111-1111-1111-1111-111111111112"}}],"academicPeriod":{"id":"11111111-1111-1111-1111-111111111113"}}
    //    var filter = new NamedQueryFilter( "registrationStatusesByAcademicPeriod" )
    //                    .WithNamedQuery( "statuses", ("detail", "id", "11111111-1111-1111-1111-111111111111"), ("detail", "id", "11111111-1111-1111-1111-111111111112") )
    //                    .WithNamedQuery( "academicPeriod", "id", "11111111-1111-1111-1111-111111111113" )
    //                    .BuildNamedQuery();
    //    Print( "NamedQuery1", filter );

    //    //?instructor={"instructor":{"id":"11111111-1111-1111-1111-111111111111"}}
    //    filter = new NamedQueryFilter( "instructor" )
    //                .WithNamedQuery( "instructor", "id", "11111111-1111-1111-1111-111111111111" )
    //                .BuildNamedQuery();
    //    Print( "NamedQuery2", filter );

    //    //?keywordSearch={"keywordSearch":"Origins"}
    //    filter = new NamedQueryFilter( "keywordSearch" )
    //                .WithNamedQuery( "keywordSearch", "Origins" )
    //                .BuildNamedQuery();
    //    Print( "NamedQuery3", filter );
    //}

    private static async Task<Persons_V12_3_0> GetPersonAsync()
    {
        Random random = new();
        int num = random.Next( 0, 499 );
        EthosResponse responses = await client.GetAsync<IEnumerable<Persons_V12_3_0>>( "persons", "", num, 1 );
        Persons_V12_3_0? person = ( responses.Dto as IEnumerable<Persons_V12_3_0> )!.FirstOrDefault();
        return person!;
    }

    private static void Print( string exampleNumber, string filter )
    {
        Console.WriteLine( $"{exampleNumber}\t: {filter}" );
    }
}

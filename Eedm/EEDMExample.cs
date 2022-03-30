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

    public static async Task Run()
    {
        BuildEthosProxyClient();
        await ExampleGetStudentCohortAsync();
        await ExampleFullCrudWithStronglyTypedAsync();
    }

    /// <summary>
    /// GET example for student-cohorts
    /// </summary>
    /// <returns></returns>
    private static async Task ExampleGetStudentCohortAsync()
    {
        var response = await proxyClient.GetAsync<IEnumerable<StudentCohorts_V7_2_0>>( "student-cohorts" );

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

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
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
            EthosResponse response = await proxyClient.PostAsync<PersonHolds_V6_0>( "person-holds", personHold1 );

            // PRINT
            var dto = response.Dto as PersonHolds_V6_0;
            Console.WriteLine( "Created a 'person-holds' record:" );
            Console.WriteLine( dto!.Id );
            Console.WriteLine();

            string newId = response.Dto.Id.ToString();
            DateTimeOffset newHoldEnd = DateTimeOffset.Now.AddDays( 1 );
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
    /// 
    /// </summary>
    /// <returns></returns>
    private static async Task<Persons_V12_3_0> GetPersonAsync()
    {
        Random random = new();
        int num = random.Next( 0, 499 );
        EthosResponse responses = await proxyClient.GetAsync<IEnumerable<Persons_V12_3_0>>( "persons", "", num, 1 );
        Persons_V12_3_0? person = ( responses.Dto as IEnumerable<Persons_V12_3_0> )!.FirstOrDefault();
        return person!;
    }

    private static void Print( string exampleNumber, string filter )
    {
        Console.WriteLine( $"{exampleNumber}\t: {filter}" );
    }
}

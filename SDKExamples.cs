/*
 * ******************************************************************************
 *   Copyright  2022 Ellucian Company L.P. and its affiliates.
 * ******************************************************************************
 */

namespace Ellucian.Examples;

public class SDKExamples
{
    public static async Task Main( string [] args )
    {
        if ( args == null || !args.Any() )
        {
            Console.WriteLine( "Please provide an API key as a program argument to run this sample program. You can set 'commandLineArgs' with the api key (a GUID) in launchSettings.json under Properties folder." );
            return;
        }
        else
        {
            if ( ! Guid.TryParse( args[0], out _ ) )
            {
                Console.WriteLine( "Please provide an API key as a program argument to run this sample program. You can set 'commandLineArgs' with the api key (a GUID) in launchSettings.json under Properties folder." );
                return;
            }
        }

        var apiKey = args [ 0 ];

        await ExampleBase.Run( apiKey );
    }
}

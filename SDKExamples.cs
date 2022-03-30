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
            Console.WriteLine( "Please provide an API key as a program argument to run this sample program." );
            return;
        }

        var apiKey = args [ 0 ];

        await ExampleBase.Run( apiKey );
    }
}

/*
 * ******************************************************************************
 *   Copyright  2022 Ellucian Company L.P. and its affiliates.
 * ******************************************************************************
 */

using Ellucian.Ethos.Integration.Client;
using Ellucian.Ethos.Integration.Notification;
using Ellucian.Ethos.Integration.Service;

namespace Ellucian.Examples;

public class EthosChangeNotificationSubscriberExample : ExampleBase
{
    private static ClientAppChangeNotificationSubscriber myChangeNotificationSubscriber = default!;
    private static ClientAppChangeNotificationSubscriber myChangeNotificationSubscriber1 = default!;

    public static async Task Run()
    {
        try
        {
            await SubscribeToChangeNotifications();
        }
        catch ( Exception e )
        {
            Console.WriteLine( e.Message );
        }

        try
        {
            await SubscribeToChangeNotificationsList();
        }
        catch ( Exception e )
        {
            Console.WriteLine( e.Message );
        }
    }

    /// <summary>
    /// Change notification polling example.
    /// </summary>
    /// <returns></returns>
    private static async Task SubscribeToChangeNotifications()
    {
        EthosClientBuilder ethosClientBuilder = GetEthosClientBuilder();
        EthosChangeNotificationService cnService = EthosChangeNotificationService.Build( action =>
        {
            action
            .WithEthosClientBuilder( ethosClientBuilder );
        }, SAMPLE_API_KEY );
        int? limit = 2;

        myChangeNotificationSubscriber = new();
        myChangeNotificationSubscriber1 = new();
        EthosChangeNotificationPollService service = new EthosChangeNotificationPollService( cnService, limit, 5 )
            .AddSubscriber( myChangeNotificationSubscriber )
            .AddSubscriber( myChangeNotificationSubscriber1 );
        await service.SubscribeAsync();
    }

    /// <summary>
    /// Change notification polling example.
    /// </summary>
    /// <returns></returns>
    private static async Task SubscribeToChangeNotificationsList()
    {
        EthosClientBuilder ethosClientBuilder = GetEthosClientBuilder();
        EthosChangeNotificationService cnService = EthosChangeNotificationService.Build( action =>
        {
            action.WithEthosClientBuilder( ethosClientBuilder );
        }, SAMPLE_API_KEY );

        int? limit = 2;

        ClientAppChangeNotificationListSubscriber subscriber = new();
        ClientAppChangeNotificationListSubscriber subscriber1 = new();
        EthosChangeNotificationListPollService service = new EthosChangeNotificationListPollService( cnService, limit, 5 )
            .AddSubscriber( subscriber )
            .AddSubscriber( subscriber1 );

        await service.SubscribeAsync();
    }
}

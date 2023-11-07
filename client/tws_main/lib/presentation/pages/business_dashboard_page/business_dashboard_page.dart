import 'package:cosmos_foundation/contracts/cosmos_page.dart';
import 'package:cosmos_foundation/foundation/simplifiers/separator_column.dart';
import 'package:flutter/material.dart';
import 'package:tws_main/config/theme/theme_base.dart';
import 'package:tws_main/presentation/widgets/tws_info_table.dart';

/// UI Page for Business Dashboard functionallity.
/// This Page is the first one that is introduced when the user authenticates itself.
///
/// Mostly used to show a quick resume of all the business data highlights.
class BusinessDashboardPage extends CosmosPage {
  const BusinessDashboardPage({super.key});

  @override
  Widget build(BuildContext context) {
    final TextStyle headerStyle = twsTheme.headerStyle.title.apply(
      color: twsTheme.primaryColor.textColor,
    );

    return SeparatorColumn(
      crossAxisAlignment: CrossAxisAlignment.start,
      spacing: 12,
      children: <Widget>[
        Padding(
          padding: const EdgeInsets.all(32),
          child: Text(
            'Active Trips',
            style: headerStyle,
          ),
        ),
        const TWSInfoTable(
          headersLinks: <TableHeaderPropertyOption>[
            TableHeaderPropertyOption('ID', 'id'),
            TableHeaderPropertyOption('Status', 'status'),
            TableHeaderPropertyOption('Customer', 'customer'),
            TableHeaderPropertyOption('Origin', 'origin'),
            TableHeaderPropertyOption('Destination', 'destination'),
            TableHeaderPropertyOption('Truck', 'truck'),
            TableHeaderPropertyOption('Driver(s)', 'driver'),
          ],
          data: <Map<String, dynamic>>[
            <String, dynamic>{
              'id': 10001,
              'status': 'in route to drop off',
              'customer': 'Coca Cola',
              'origin': 'Coca 1. El florido, 3377Coca 1. El florido, 3377Coca 1. El florido, 3377Coca 1. El florido, 3377',
              'destination': 'Coca 3. Los Angeles',
              'truck': 'TW137',
              'driver': 'Dev Driver',
            },
            <String, dynamic>{
              'id': 10001,
              'status': 'in route to drop off',
              'customer': 'Coca Cola',
              'origin': 'Coca 1. El florido, 3377Coca 1. El florido, 3377Coca 1. El florido, 3377Coca 1. El florido, 3377',
              'destination': 'Coca 3. Los Angeles',
              'truck': 'TW137',
              'driver': 'Dev Driver',
            },
          ],
        ),
      ],
    );
  }
}

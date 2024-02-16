import 'package:cosmos_foundation/contracts/cosmos_page.dart';
import 'package:flutter/material.dart';

class EntryPage extends CosmosPage {
  const EntryPage({super.key});

  @override
  Widget compose(BuildContext ctx, Size window) {
    return const ColoredBox(
      color: Colors.red,
    );
  }
}

import 'package:cosmos_foundation/contracts/cosmos_layout.dart';
import 'package:flutter/material.dart';

class MasterLayout extends CosmosLayout {
  const MasterLayout({
    super.key,
    required super.page,
  });

  @override
  Widget build(BuildContext context) {
    return ColoredBox(
      color: Colors.green,
      child: page,
    );
  }
}

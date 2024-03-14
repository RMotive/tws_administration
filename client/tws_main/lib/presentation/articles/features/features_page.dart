import 'package:cosmos_foundation/contracts/cosmos_page.dart';
import 'package:cosmos_foundation/foundation/simplifiers/colored_sizedbox.dart';
import 'package:flutter/material.dart';

class FeaturesArticle extends CosmosPage {
  const FeaturesArticle({super.key});

  @override
  Widget compose(BuildContext ctx, Size window) {
    return const CosmosColorBox(
      size: Size(200, 200),
      color: Colors.transparent,
      child: Center(
        child: ColoredBox(
          color: Colors.grey,
          child: Padding(
            padding: EdgeInsets.all(12.0),
            child: Text('WELCOME TO FEATURES MANAGEMENT LEAF'),
          ),
        ),
      ),
    );
  }
}

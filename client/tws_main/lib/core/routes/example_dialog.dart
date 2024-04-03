import 'package:cosmos_foundation/contracts/cosmos_page.dart';
import 'package:cosmos_foundation/contracts/cosmos_route_node.dart';
import 'package:cosmos_foundation/foundation/simplifiers/colored_sizedbox.dart';
import 'package:flutter/material.dart';

class ExampleDialog extends CosmosRouteNode {
  ExampleDialog(super.routeOptions)
      : super(
          pageBuild: (_, __) {
            return const ExamplePage();
          },
          transitionBuild: (CosmosPage page) {
            return DialogPage<dynamic>(
              builder: (_) {
                return page;
              },
            );
          },
        );
}

class DialogPage<T> extends Page<T> {
  final Offset? anchorPoint;
  final Color? barrierColor;
  final bool barrierDismissible;
  final String? barrierLabel;
  final bool useSafeArea;
  final CapturedThemes? themes;
  final WidgetBuilder builder;

  const DialogPage({
    required this.builder,
    this.anchorPoint,
    this.barrierColor = Colors.black87,
    this.barrierDismissible = true,
    this.barrierLabel,
    this.useSafeArea = true,
    this.themes,
    super.key,
    super.name,
    super.arguments,
    super.restorationId,
  });

  @override
  Route<T> createRoute(BuildContext context) {
    return DialogRoute<T>(
      context: context,
      settings: this,
      builder: (BuildContext context) => Dialog(
        backgroundColor: Colors.transparent,
        shadowColor: Colors.transparent,
        elevation: 0,
        child: builder(context),
      ),
      anchorPoint: anchorPoint,
      barrierColor: Colors.transparent,
      barrierDismissible: barrierDismissible,
      barrierLabel: barrierLabel,
      useSafeArea: useSafeArea,
    );
  }
}

class ExamplePage extends CosmosPage {
  const ExamplePage({super.key});

  @override
  Widget compose(BuildContext ctx, Size window) {
    return const Center(
      child: CosmosColorBox(
        size: Size(400, 100),
        color: Colors.red,
      ),
    );
  }
}

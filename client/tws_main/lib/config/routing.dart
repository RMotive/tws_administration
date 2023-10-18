import 'package:flutter/material.dart';
import 'package:go_router/go_router.dart';
import 'package:tws_main/views/screens/access_screen.dart';

class Routing extends GoRouter {
  Routing()
      : super(
          routes: <RouteBase>[
            GoRoute(
              path: '/',
              builder: (BuildContext ctx, _) => const AccessScreen(),
            ),
          ],
        );
}

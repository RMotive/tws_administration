import 'package:go_router/go_router.dart';
import 'package:tws_main/view/login_view/login_view.dart';

///
final routingConfig = GoRouter(
  routes: <RouteBase>[
    GoRoute(
      path: '/',
      builder: (ctx, _) => const LoginScreen(),
    ),
  ],
);

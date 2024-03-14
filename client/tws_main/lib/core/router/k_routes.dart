import 'package:cosmos_foundation/models/options/route_options.dart';

/// Stores all the route options objects for use the [RouteDriver] and
/// initialize the route tree of the application.
///
/// NOTE: remember that if you want to use the [RouteDriver] you should use this instance objects
/// case that manager uses a combination of calculations and hashcode of [RouteOptions] objects
/// to generate absolute pathing and another behaviors.
class KRoutes {
  static const RouteOptions loginPage = RouteOptions('/');
  static const RouteOptions overviewPage = RouteOptions('landing');
  static const RouteOptions securityPage = RouteOptions('security');
  static const RouteOptions securityPageFeaturesArticle = RouteOptions('articles');
}

import 'package:cosmos_foundation/router/router_module.dart';

/// Stores all the route options objects for use the [RouteDriver] and
/// initialize the route tree of the application.
///
/// NOTE: remember that if you want to use the [RouteDriver] you should use this instance objects
/// case that manager uses a combination of calculations and hashcode of [RouteOptions] objects
/// to generate absolute pathing and another behaviors.
class TWSAKRoutes {
  static const CSMRouteOptions loginPage = CSMRouteOptions('/');
  static const CSMRouteOptions overviewPage = CSMRouteOptions('overview');
  static const CSMRouteOptions securityPage = CSMRouteOptions('security');
  static const CSMRouteOptions featuresArticle = CSMRouteOptions('features');
  static const CSMRouteOptions featuresCreateWhisper = CSMRouteOptions('create');
  static const CSMRouteOptions businessPage = CSMRouteOptions('business');
  static const CSMRouteOptions trucksArticle = CSMRouteOptions('trucks');
  static const CSMRouteOptions trucksCreateWhisper = CSMRouteOptions('add');

}

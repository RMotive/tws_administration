import 'package:csm_foundation_view/csm_foundation_view.dart';

/// Stores all the route options objects for use the [RouteDriver] and
/// initialize the route tree of the application.
///
/// NOTE: remember that if you want to use the [RouteDriver] you should use this instance objects
/// case that manager uses a combination of calculations and hashcode of [RouteOptions] objects
/// to generate absolute pathing and another behaviors.
class TWSARoutes {
  static const CSMRouteOptions loginPage = CSMRouteOptions('/');
  static const CSMRouteOptions overviewPage = CSMRouteOptions('overview');
  static const CSMRouteOptions securityPage = CSMRouteOptions('security');

  static const CSMRouteOptions solutionsArticle = CSMRouteOptions('solutions');

  static const CSMRouteOptions featuresArticle = CSMRouteOptions('features');
  static const CSMRouteOptions featuresCreateWhisper = CSMRouteOptions('create');
}

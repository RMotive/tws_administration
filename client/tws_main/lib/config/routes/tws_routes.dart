import 'package:cosmos_foundation/models/options/route_options.dart';

/// Stores the [RouteOptions] for auth page,
///
/// This route is used to the user can firm itself and ensure the correct credentials validation
/// before it get access to the application functionallities,
const RouteOptions accessRoute = RouteOptions(
  '',
  name: 'auth',
);

/// Stores the [RouteOptions] for business dashboard page
/// Screen:
///   [MainScreen]
///
/// This page is the initial point where the application drives the user when it successful firms its credentials.
/// This page shows a quickly overview of the current business highlights.
const RouteOptions businessDashboardRoute = RouteOptions(
  'business-dashboard',
  name: 'bdashboard',
);

/// Stores the [RouteOptions] for application settings.
/// Screen:
///   [MainScreen]
///
/// This page is used to change several application options.
const RouteOptions applicationSettingsRoute = RouteOptions(
  'application-settings',
  name: 'asettings',
);

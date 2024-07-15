part of '../../master_layout.dart';

/// [_SessionStorge] Instance initialization of storage session.
final SessionStorage _sessionStorage = SessionStorage.i;

/// [_contact] Stores the user contact information.
final Contact _contact = _sessionStorage.session!.contact;

/// [_fullName] Stores the contact name.
String get _fullName => "${_contact.name} ${_contact.lastname}";

/// [_IsHovered] Boolean that stores the current state for the main header button.
bool _isHovered = false;

/// [_animationDuration] Set the animations duration for the component.
const Duration _animationDuration = Duration(milliseconds: 100);

/// [_headerMenuStyle] Stores the text style for the header on the menu overlay.
TextStyle _headerMenuStyle = const TextStyle(
  fontSize: 12,
  overflow: TextOverflow.ellipsis,
);

/// [_options] Set the action buttons configuration for the menu overlay.
final List<_Options> _options = <_Options>[
  const _Options(
    title: 'Profile',
    icon: Icons.engineering,
    route: TWSARoutes.profile,
  ),
  const _Options(
    title: 'Settings',
    icon: Icons.settings,
    route: TWSARoutes.settings,
  ),
  const _Options(
    title: 'About',
    icon: Icons.more_vert,
    route: TWSARoutes.about,
  )
];

/// [_getInitials] Returns the user initials, based on [Contact.name] & [Contact.lastname].
/// To get user initials, this method takes the first letter from [Contact.name] and the first letter for each word in [Contact.lastname].
String _getInitials() {
  String initials = _contact.name.substring(0, 1);
  List<String> lastname = _contact.lastname.split(" ");
  for (String word in lastname) {
    initials += word.substring(0, 0);
  }
  initials.toUpperCase();
  return initials;
}

/// [_MasterUserButtonState] CSMState Class for a proper state [CSMDynamicWidget] management.
final class _MasterUserButtonState extends CSMStateBase {
  /// [_overlayController] Overlay menu controller. Set and stores the state for overlay menu.
  final OverlayPortalController _overlayController = OverlayPortalController();
}

/// [_Options] A private Class that stores the overlay menu options configuration.
final class _Options {
  final String title;
  final IconData icon;
  final CSMRouteOptions route;
  final bool suffix;
  const _Options({required this.title, required this.icon, required this.route, this.suffix = true});
}

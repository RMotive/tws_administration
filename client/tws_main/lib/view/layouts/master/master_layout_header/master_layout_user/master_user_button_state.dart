
part of 'master_user_button.dart';

final class _MasterUserButtonState extends CSMStateBase { }

TextStyle _headerMenuStyle = const TextStyle(
  fontSize: 12,
  overflow: TextOverflow.ellipsis,
);

bool _isHovered = false;
final OverlayPortalController _overlayController = OverlayPortalController();
const Duration _animationDuration = Duration(milliseconds: 100);

final class _Options{
  final String title;
  final IconData icon;
  final dynamic route;
  final bool suffix;
  const _Options({
    required this.title,
    required this.icon,
    required this.route,
    this.suffix = false
   }
  );
}

List<_Options> _options = <_Options>[
  const _Options(
    title: 'Profile',
    icon: Icons.engineering, 
    route: '',
    suffix: true
  ),
  const _Options(
    title: 'Settings',
    icon: Icons.settings, 
    route: '',
    suffix: true
  ),
  const _Options(
    title: 'About',
    icon: Icons.more_vert, 
    route: '',
    suffix: true
  ),
  const _Options(
    title: 'Log Out',
    icon: Icons.logout, 
    route: '',
  ),
];
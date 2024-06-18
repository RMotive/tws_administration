
part of '../../master_layout.dart';

SessionStorage _sessionStorage = SessionStorage.i;

bool _isHovered = false;
const Duration _animationDuration = Duration(milliseconds: 100);
TextStyle _headerMenuStyle = const TextStyle(
  fontSize: 12,
  overflow: TextOverflow.ellipsis,
);

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

final class _MasterUserButtonState extends CSMStateBase { 
  final OverlayPortalController _overlayController = OverlayPortalController();
}

final class _Options{
  final String title;
  final IconData icon;
  final CSMRouteOptions route;
  final bool suffix;
  const _Options({
    required this.title,
    required this.icon,
    required this.route,
    this.suffix = true
   }
  );
}
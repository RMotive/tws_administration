
part of '../../master_layout.dart';

typedef _State = _MasterUserButtonState;

class MasterUserButton extends StatelessWidget {
  final double width;
  final double height;
  final double menuWidth;
  final double menuHeight;
  final CSMColorThemeOptions themeOptions;

  const MasterUserButton({
    super.key,
    this.width = 80,
    this.height = 50,
    this.menuWidth = 220,
    this.menuHeight = 300,
    required this.themeOptions
  });

  @override
  Widget build(BuildContext context) {
    return CSMDynamicWidget<_State>(
      state: _State(),
      designer: (BuildContext ctx, _State state) {
        return Tooltip(
          message: "Account Options",
          child: MouseRegion(
            onEnter: (PointerEnterEvent event) {
              _isHovered = true;
              state.effect();
            },
            onExit: (PointerExitEvent event) {
              _isHovered = false;
              state.effect();
            },
            child: OverlayPortal(
              overlayChildBuilder: (_) {
                return Positioned(
                  top: 55,
                  right: 12,
                  child: TapRegion(
                    onTapOutside: (_) {
                      if(!_isHovered){
                        state._overlayController.hide();
                        state.effect();
                      }
                    },
                    child: _ContentMenu(
                      width: menuWidth,
                      height: menuHeight,
                      themeOptions: themeOptions,
                      onSelectOption: (CSMRouteOptions  selectedRoute) {
                        _routeDriver.drive(selectedRoute);
                        state._overlayController.hide();
                        state.effect();
                      },
                    ),
                  ),
                );
              },
              controller: state._overlayController,
              child: TapRegion(
                onTapInside: (_) {
                  state._overlayController.toggle();
                  state.effect();                 
                },
                child: AnimatedContainer(
                  duration: _animationDuration,
                  height: height,
                  width:state. _overlayController.isShowing
                      ? menuWidth
                      : width,
                  decoration: BoxDecoration(
                      color: _isHovered? themeOptions.fore :themeOptions.hightlightAlt ?? Colors.white,
                      borderRadius: state._overlayController.isShowing? BorderRadius.circular(10) : BorderRadius.circular(30)), 
                  child: Center(
                      child: Text(
                    "EPG",
                    style: TextStyle(
                        color: themeOptions.main
                      ),
                  )),
                ),
              ),
            ),
          ),
        );
      },
    );
  }
}

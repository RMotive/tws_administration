import 'package:csm_view/csm_view.dart';
import 'package:flutter/material.dart';

class _TWSListTileState extends CSMStateBase {}
_TWSListTileState _state = _TWSListTileState();
class TWSListTile extends StatelessWidget {
  final double? width;
  final double? height;
  final void Function()? onTap;
  final Color backgroundColor;
  final TextAlign textAlignment;
  final EdgeInsetsGeometry padding;
  final String label;
  final Color textColor;
  final Color? onHoverColor;
  final Color? onHoverTextColor;
  const TWSListTile({
    super.key,
    required this.label,
    this.width,
    this.height,
    this.onTap,
    this.textColor = Colors.black,
    this.onHoverColor,
    this.onHoverTextColor,
    this.backgroundColor = Colors.transparent,
    this.textAlignment = TextAlign.left,
    this.padding = const EdgeInsets.all(5),
  });

  @override
  Widget build(BuildContext context) {
    Color tcolor = textColor;
    Color bcolor = backgroundColor;
    return SizedBox(
      height:height ,
      width: width,
      child: CSMDynamicWidget<_TWSListTileState>(
        state: _state,
        designer:(_, _TWSListTileState state) {
          return  CSMPointerHandler(
          cursor:SystemMouseCursors.click,
          onHover: (bool hover) {
            if(hover){
              tcolor = onHoverTextColor ?? textColor;
              bcolor = onHoverColor ?? backgroundColor;
            }else{
              tcolor = textColor;
              bcolor = backgroundColor;
            }
            state.effect();
          },
          onClick:onTap,
          child: ColoredBox(
            color: bcolor,
            child: Padding(
              padding: padding,
              child: Text(
                textAlign: textAlignment,
                softWrap: false,
                label,
                style: TextStyle(
                  color: tcolor,
                ),
              ),
            ),
          ),
        );} ,
      ),
    );
  }
}

import 'package:csm_view/csm_view.dart';
import 'package:flutter/material.dart';



class TwsListTile extends StatefulWidget {
  /// Tile width.
  final double? width;
  /// Tile heigth
  final double? height;
  /// Method triggered on tile selection.
  final void Function(bool selected)? onTap;
  /// Background color.
  final Color backgroundColor;
  /// Text alignement for tile text.
  final TextAlign textAlignment;
  /// Custom padding for tile content.
  final EdgeInsetsGeometry padding;
  /// Tile text content 
  final String label;
  /// Text color.
  final Color textColor;
  /// Tile background color when hover event is triggered.
  final Color? onHoverColor;
  /// Tile text color when hover event is triggered.
  final Color? onHoverTextColor;
  /// Optional method to evaluate if the tile is selected.
  final bool Function()? evaluateSelection;
  /// Flag for tile status.
  final bool enabled;
  const TwsListTile({ super.key,
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
    this.evaluateSelection,
    this.enabled = true,
  });

  @override
  State<TwsListTile> createState() => _TwsListTileState();
}

class _TwsListTileState extends State<TwsListTile> {
  /// Text color.
  late Color tcolor;
  /// Background color.
  late Color bcolor;
  /// Selected status.
  late bool selected;

  @override
  void initState() {
    tcolor = widget.textColor;
    bcolor = widget.backgroundColor;
    selected = false;
    // Set the selected color if [evaluateSelection] is not null and returns true.
    if(widget.evaluateSelection != null) selected = widget.evaluateSelection!();
  
    super.initState();
  }
  
  @override
  void didUpdateWidget(covariant TwsListTile oldWidget) {
    /// Evaluate the selected status.
    if(widget.evaluateSelection != null && widget.enabled){
      selected = widget.evaluateSelection!();
    } else if(!widget.enabled){
      selected = false;
    }
    super.didUpdateWidget(oldWidget);
  }

  @override
  Widget build(BuildContext context) {
    tcolor = widget.textColor;
    bcolor = widget.backgroundColor;
    if(selected){
      tcolor = widget.onHoverTextColor ?? widget.textColor;
      bcolor = widget.onHoverColor ?? widget.backgroundColor;
    }

    return SizedBox(
      height:widget.height,
      width: widget.width,
      child: CSMPointerHandler(
        cursor: widget.enabled? SystemMouseCursors.click : SystemMouseCursors.basic,
        onHover: (bool hover) {
          if(!widget.enabled) return;

          setState(() {
            if(hover){
              tcolor = widget.onHoverTextColor ?? widget.textColor;
              bcolor = widget.onHoverColor ?? widget.backgroundColor;
            } else {
              tcolor = widget.textColor;
              bcolor = widget.backgroundColor;
            }
          });
        },
        onClick:() {
          if(!widget.enabled) return;

          if(selected){
            selected = false;
          }else{
            selected = true;
          }
          setState(() {
            if(widget.onTap != null) widget.onTap!(selected);
          });
        },
        child: ColoredBox(
          color: bcolor,
          child: Padding(
            padding: widget.padding,
            child: Text(
              textAlign: widget.textAlignment,
              softWrap: true,
              widget.label,
              maxLines: 2,
              style: TextStyle(
                color: tcolor,
                overflow: TextOverflow.ellipsis,
              ),
            ),
          ),
        ),
      )
    );
  }
}

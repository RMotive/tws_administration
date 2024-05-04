
import 'package:flutter/material.dart';

///[TWSSwitchButton] Custom widget for TWS environment.
///This widget returns a boolean, based on the its state.



class TWSSwitchButton extends StatefulWidget {
    // @override
    //   final GlobalKey key = GlobalKey<_TWSSwitchButtonState>();

  final double width;
  final double height;
  final bool value;
  final String title;
  final void Function(bool)? onChanged;
  const TWSSwitchButton({super.key,
    required this.title,
    required this.onChanged,
    this.width = 70,
    this.height = 60,
    this.value = false
  });
  
  
  @override
  State<TWSSwitchButton> createState() => _TWSSwitchButtonState();
}
bool _internalValue = false;

class _TWSSwitchButtonState extends State<TWSSwitchButton> {

  @override
  void initState() {
    _internalValue = widget.value;
    super.initState();
  }

  void changeValue(bool value){
    setState(() {
      _internalValue = value;
    });
  }

  @override
  Widget build(BuildContext context) {
    return SizedBox(
      height: widget.height,
      width:widget.width,
      child: Column(
        children: <Widget>[
          Text(widget.title),
          Switch(
            value: _internalValue,
            onChanged: (bool change) {
              setState(() {
                _internalValue = change;
                widget.onChanged!(change);
              });
             
            },
          ),
        ],
      ),
    );
  }
}

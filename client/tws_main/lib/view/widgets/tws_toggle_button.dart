
import 'package:csm_foundation_view/csm_foundation_view.dart';
import 'package:flutter/material.dart';

final class _SwitchState extends CSMStateBase {
 final GlobalKey state = GlobalKey();
}

typedef _State = _SwitchState;
dynamic _value;
class TWSToggleButton extends StatelessWidget {
  final double width;
  final double height;
  final bool value;
  final String? title;
  // Return the boolean value of the selected option.
  final void Function(bool) onSelected;
  const TWSToggleButton({super.key,
    required this.onSelected,
    this.title,
    this.width = 70,
    this.height = 60,
    this.value = false
  });
  
  @override
  Widget build(BuildContext context) {
    return  CSMDynamicWidget<_State>(
      state: _State(),
        designer: (BuildContext ctx, CSMStateBase state) {
        return SizedBox( 
        width: width,
        height: height,
        child: Column(
          children: <Widget>[
            Visibility(
              visible:   title != null,
              child: Text(title ?? "")
            ),
                Row(
                  children: <Widget>[
                    TapRegion(
                        onTapInside: (_) {
                          _value = false;
                          onSelected(_value);
                          state.effect();
                        },
                        child: const DecoratedBox(
                          decoration: BoxDecoration(color: Colors.amber),
                          child: Text("Tap on me"),
                        )),
                    TapRegion(
                        onTapInside: (_) {
                          _value = false;
                          onSelected(_value);
                          state.effect();
                        },
                        child: const DecoratedBox(
                          decoration: BoxDecoration(color: Colors.amber),
                          child: Text("Tap on me"),
                        ))
                  ],
            )
          ],
        ),
      );
      }
    );
    
  }
}



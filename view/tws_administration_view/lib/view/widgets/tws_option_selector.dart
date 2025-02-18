import 'package:flutter/material.dart';
import 'package:tws_administration_view/view/widgets/tws_button_flat.dart';

/// Custom class for Options/Action implementations on [TwsOptionSelector] widget.
class TwsOptionSelectorAction<T>{
  // Action title.
  final String title;
  // Action value. Returned when action is selected.
  final T value;
  // Min width.
  final double minWidth;
  // Max width
  final double maxWidth;

  const TwsOptionSelectorAction({
    required this.title,
    required this.value,
    this.minWidth = double.maxFinite,
    this.maxWidth = double.maxFinite,
  });
}

/// [TwsOptionSelector] Width that display a [Wrap] that contains a list of selectable actions given in [options] property.
class TwsOptionSelector<T> extends StatefulWidget {
  /// Actions list to display in this widget.
  final List<TwsOptionSelectorAction<T>> options;
  /// Trigger function on action selection.
  final Function(T value) onSelect;
  /// Preselected action.
  final T? initialValue;
  /// Widget status flag.
  final bool enabled;

  const TwsOptionSelector({ super.key,
    required this.options,
    required this.onSelect,
    this.initialValue,
    this.enabled = true,
  });

  @override
  State<TwsOptionSelector<T>> createState() => _TwsOptionSelectorState<T>();
}

class _TwsOptionSelectorState<T> extends State<TwsOptionSelector<T>> {
  /// Internal selected option.
  late T? selected;

  @override
  void initState() {
    selected = widget.initialValue ?? widget.options.first.value;
    super.initState();
  }
  @override
  void didUpdateWidget(covariant TwsOptionSelector<T> oldWidget) {
    if(selected != widget.initialValue){
      selected = widget.initialValue ?? selected;
    }
    super.didUpdateWidget(oldWidget);
  }

  @override
  Widget build(BuildContext context) {
    return LayoutBuilder(
      builder: (BuildContext context, BoxConstraints constraints) {
        /// Calculating size for each action in [options] property.
        double availableWidth = constraints.maxWidth;
        double spacing = 5;
        double calculatedSpace =
            (availableWidth / widget.options.length) - (spacing * widget.options.length);
        return Center(
          child: Wrap(
            runAlignment: WrapAlignment.center,
            crossAxisAlignment: WrapCrossAlignment.center,
            spacing: 10,
            runSpacing: 10,
            children: <ConstrainedBox>[
              for (int i = 0; i < widget.options.length; i++)
                ConstrainedBox(
                  constraints: BoxConstraints(
                    minWidth: widget.options[i].minWidth,
                    maxWidth: widget.options[i].maxWidth,
                  ),
                  child: TWSButtonFlat(
                    width: calculatedSpace,
                    label: widget.options[i].title,
                    disabled: widget.initialValue != null
                        ? widget.options[i].value == selected
                        : (widget.options[i].value == selected) || (widget.enabled),
                    onTap: () {
                      setState(() {
                        if(!widget.enabled) return;
                        selected = widget.options[i].value;
                        widget.onSelect(widget.options[i].value);
                      });
                    },
                  ),
                ),
            ],
          ),
        );
      },
    );
  }
}

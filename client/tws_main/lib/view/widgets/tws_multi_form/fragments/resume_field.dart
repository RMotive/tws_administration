part of "../tws_multi_form.dart";

class _ResumeField extends StatelessWidget {
  final CollectorData data;
  final double maxWidth;
  final double minWidth;
  final EdgeInsets padding;
  final TextStyle? titleStyle;
  final TextStyle? subtitleStyle;
  const _ResumeField({this.padding = const EdgeInsets.all(8.0), this.minWidth = 0, required this.data, required this.maxWidth, this.titleStyle, this.subtitleStyle});

  @override
  Widget build(BuildContext context) {
    bool isError = data.error != null;

    return Padding(
      padding: padding,
      child: ConstrainedBox(
        constraints: BoxConstraints(maxWidth: maxWidth, minWidth: minWidth),
        child: Column(
          children: <Widget>[
            Text(data.title, style: titleStyle),
            Text(
                data.value == false
                    ? "Deactivated"
                    : data.value == true
                        ? "Activated"
                        : data.value ?? " ",
                style: subtitleStyle),
            Visibility(
              visible: isError,
              child: Text(data.error.toString(), style: subtitleStyle?.copyWith(color: Colors.red)),
            )
          ],
        ),
      ),
    );
  }
}

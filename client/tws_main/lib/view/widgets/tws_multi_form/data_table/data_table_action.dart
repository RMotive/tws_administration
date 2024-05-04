part of "../tws_multi_form.dart";

class _DataTableAction extends StatelessWidget {
  final String tooltip;
  final IconData icon;

  final CSMStateThemeOptions struct;
  final Function() action;

  const _DataTableAction({
    required this.tooltip,
    required this.icon,
    required this.action,
    required this.struct,
  });

  @override
  Widget build(BuildContext context) {
    return TWSButtonChip(
      size: 30,
      struct: struct,
      tooltip: tooltip,
      decorator: (double size, Color color) {
        return Icon(
          icon,
          color: color,
          size: size * .7,
        );
      },
      action: action,
    );
  }
}

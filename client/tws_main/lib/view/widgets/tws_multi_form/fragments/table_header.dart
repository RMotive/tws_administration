part of "../tws_multi_form.dart";

class _CollectorHeader extends StatelessWidget {
  const _CollectorHeader();

  @override
  Widget build(BuildContext context) {
    return SizedBox(
      height: 35,
      child: Row(
        children: <Widget>[
          _HeaderChip(
              tooltip: "Add Truck Row",
              icon: Icons.playlist_add,
              action: () {
                _addItem();
                _formKey.currentState!.validate();
                _globalState.effect();
              }),
          Padding(
              padding: const EdgeInsets.symmetric(horizontal: 8.0),
              child: _HeaderChip(
                tooltip: "Delete selected item",
                icon: Icons.delete_forever,
                action: () {
                  _deleteItem(_selectedItem);
                  _globalState.effect();
                },
              )),
          const _HeaderChip(tooltip: "Show Data", icon: Icons.save, action: _retriveData)
        ],
      ),
    );
  }
}

class _HeaderChip extends StatelessWidget {
  final String tooltip;
  final IconData icon;
  final Function() action;

  const _HeaderChip({
    required this.tooltip,
    required this.icon,
    required this.action,
  });

  @override
  Widget build(BuildContext context) {
    return TWSButtonChip(
        size: 30,
        struct: _struct,
        tooltip: tooltip,
        decorator: (double size, Color color) {
          return Icon(
            icon,
            color: color,
            size: size * .7,
          );
        },
        action: action);
  }
}

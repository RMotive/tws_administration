part of "../tws_multi_form.dart";

class _TableActionsRibbon extends StatelessWidget {
  final void Function() add;
  final void Function() remove;
  final void Function() show;

  const _TableActionsRibbon({
    required this.add,
    required this.remove,
    required this.show,
  });

  @override
  Widget build(BuildContext context) {
    final CSMStateThemeOptions primary = getTheme<TWSAThemeBase>().primaryControlState;
    final CSMStateThemeOptions critical = getTheme<TWSAThemeBase>().criticalControlState;

    return SizedBox(
      height: 35,
      child: Row(
        children: <Widget>[
          _DataTableAction(
            tooltip: "Add",
            struct: primary,
            icon: Icons.playlist_add,
            action: () {
              _addItem();
              _formKey.currentState!.validate();
              _globalState.effect();
            },
          ),
          Padding(
            padding: const EdgeInsets.symmetric(horizontal: 8.0),
            child: _DataTableAction(
              tooltip: "Remove",
              struct: critical,
              icon: Icons.delete_forever,
              action: () {
                _deleteItem(_selectedItem);
                _globalState.effect();
              },
            ),
          ),
          if (kDebugMode)
            _DataTableAction(
              tooltip: "Show",
              struct: primary,
              icon: Icons.save,
              action: _retriveData,
            ),
        ],
      ),
    );
  }
}

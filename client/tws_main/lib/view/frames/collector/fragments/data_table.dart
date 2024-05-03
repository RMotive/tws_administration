part of "../collector_frame.dart";

/// [_DataTable] Widget class that shows the table data section.
/// In this section incluide some buttons options to add or delete rows of items
/// & a list of items with its asigned data by the [_ValueDetails] section.
class _DataTable extends StatelessWidget {
  final double listHeight;
  final String itemName;
  const _DataTable({
    required this.listHeight,
    required this.itemName
  });

  @override
  Widget build(BuildContext context) {
    return TWSSection(
      padding: const EdgeInsets.symmetric(horizontal: 5, vertical: 20),
      title: "Table Data",
      content: Column(
        children: <Widget>[
          // Options Header
          const _CollectorHeader(),
          
          //Sub-Header
          Container(
            width: double.maxFinite,
            height: 40,
            color: _pageTheme.highlight,
            alignment: Alignment.centerLeft,
            padding: const EdgeInsets.symmetric(horizontal: 10),
            child: Row(
              mainAxisAlignment: MainAxisAlignment.spaceBetween,
              children: <Widget>[
                Text("ADD $itemName", style: _headerStyle),
                Text("Total items: ${_tableValues.length}", style: _headerStyle)
              ],
            ),
          ),

          // Main ListView. Shows the current list of items to save.
          Material(
            color: _pageTheme.foreAlt,
            child: Container(
              height: listHeight,
              constraints: const BoxConstraints(
                minHeight: _tdListsMinHeigth,
              ),
              decoration: BoxDecoration(
                border: Border.fromBorderSide(
                  BorderSide(
                    color: _pageTheme.highlight,
                    width: 2,
                  ),
                ),
              ),
              child: ListView.builder(
                  itemCount: _tableValues.length,
                  itemBuilder: (_, int index) {
                    return ListTile(
                      selected: _selectedItem == index,
                      enabled: _selectedItem != index,
                      hoverColor: _pageTheme.main,
                      selectedTileColor: _pageTheme.highlight,
                      onTap: () {
                        _selectItem(index);
                        _globalState.effect();
                      },
                      subtitle: Wrap(
                        children: _generateLabels(index)
                      ),
                      title: Padding(
                        padding: const EdgeInsets.symmetric(horizontal: 8.0),
                        child: Column(
                          crossAxisAlignment: CrossAxisAlignment.start,
                          children: <Widget>[
                            Text(
                              "#${index + 1} $itemName",
                              style: TextStyle(
                                  fontSize: 16,
                                  fontWeight: FontWeight.bold,
                                  color: _pageTheme.fore),
                            )
                          ],
                        ),
                      ),
                    );
                  }),
            ),
          ),
        ],
      ),
    );
  }
}

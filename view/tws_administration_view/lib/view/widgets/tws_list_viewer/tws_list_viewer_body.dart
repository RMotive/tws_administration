part of 'tws_list_viewer.dart';

/// Content widget for [TwsListViewer]. Displays a column with a header and a text list.
class _TwsListViewerBody<T> extends StatelessWidget {
  final Widget? customHeader;
  final List<T> content;
  final Color tColor;
  final Color bColor;
  final String title;
  final String Function(T set) tileTitle;  
  final double? heigth;
  final TextAlign titleAlignment;
  final String emptyContentMessage;
  final EdgeInsetsGeometry padding;

  const _TwsListViewerBody({
    required this.content,
    required this.tColor,
    required this.bColor,
    required this.heigth,
    required this.titleAlignment,
    required this.emptyContentMessage,
    required this.padding,
    required this.title,
    required this.tileTitle,
    this.customHeader,

  });

  @override
  Widget build(BuildContext context) {
    return CSMSpacingColumn(
      spacing: 5,
      children: <Widget>[
        customHeader != null? customHeader!:
        CSMSpacingRow(
          spacing: 10,
          mainAlignment: MainAxisAlignment.end,
          children: <Widget>[
            Text(
              "$title: ",
              style: TextStyle(
                color: tColor,
              ),
            ),
            Text(
              content.length.toString(),
              style: TextStyle(
                color: tColor,
              ),
            )
          ],
        ),
        const Divider(),
        content.isNotEmpty? SizedBox(
          height: heigth,
          child: SingleChildScrollView(
            child: Column(
              children: List<Widget>.generate(content.length,
              (int index){
                return ColoredBox(
                  color: bColor,
                  child: SizedBox(
                    width: double.maxFinite,
                    child: Padding(
                      padding: padding,
                      child: Text(
                        textAlign: titleAlignment,
                        tileTitle(content[index]),
                        style: TextStyle(
                          color: tColor,
                          fontSize: 15,
                        ),
                      ),
                    ),
                  ),
                );
              }),
            ),
          ),
        )
        : SizedBox(
          height: 50,
          child: TWSDisplayFlat(
            color: TWSAColors.oceanBlue,
            foreColor: TWSAColors.warmWhite,
            display: emptyContentMessage,
          ),
        ),
      ],
    );
  }
}
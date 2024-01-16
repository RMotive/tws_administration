part of './login_page.dart';

class _SeparatorDecorator extends StatelessWidget {
  const _SeparatorDecorator();

  @override
  Widget build(BuildContext context) {
    return LayoutBuilder(
      builder: (BuildContext context, BoxConstraints bounds) {
        return const DecoratedBox(
          decoration: BoxDecoration(
            border: Border.fromBorderSide(
              BorderSide(
                color: Colors.grey,
                width: 1,
              ),
            ),
          ),
          child: FractionallySizedBox(
            heightFactor: .5,
            widthFactor: .1,
            alignment: Alignment.center,
            child: SizedBox(),
          ),
        );
      },
    );
  }
}

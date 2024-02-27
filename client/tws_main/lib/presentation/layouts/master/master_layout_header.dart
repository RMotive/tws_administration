part of 'master_layout.dart';

class _MsaterLayoutHeader extends StatelessWidget {
  const _MsaterLayoutHeader();

  @override
  Widget build(BuildContext context) {
    final ThemeColorStruct themeStruct = getTheme<ThemeBase>().masterLayoutStruct;

    return Container(
      color: themeStruct.mainColor,
      height: 50,
    );
  }
}

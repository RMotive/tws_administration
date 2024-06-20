part of 'tws_article_table.dart';

class _TWSArticleTableLoading extends StatelessWidget {
  const _TWSArticleTableLoading();

  @override
  Widget build(BuildContext context) {
    return const Padding(
      padding: EdgeInsets.only(
        top: 50,
      ),
      child: SizedBox(
        width: 50,
        height: 50,
        child: CircularProgressIndicator(
          backgroundColor: TWSAColors.oceanBlueH,
          color: TWSAColors.oceanBlue,
          strokeWidth: 3,
        ),
      ),
    );
  }
}

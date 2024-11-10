part of 'tws_article_table.dart';

class _TWSArticleTableLoading extends StatelessWidget {
  final Size viewSize;
  const _TWSArticleTableLoading({
      required this.viewSize
  });

  @override
  Widget build(BuildContext context) {
    const double size = 50; 
    return Padding(
      padding: EdgeInsets.symmetric(
        horizontal: (viewSize.width/2) - (size/2),
        vertical: (viewSize.height/2) - (size*2)
      ),
      child: const SizedBox(
        width: size,
        height: size,
        child: CircularProgressIndicator(
          backgroundColor: TWSAColors.oceanBlueH,
          color: TWSAColors.oceanBlue,
          strokeWidth: 3,
        ),
      ),
    );
  }
}

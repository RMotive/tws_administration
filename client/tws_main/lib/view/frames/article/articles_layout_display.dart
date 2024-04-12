part of 'article_frame.dart';

/// [Private] component.
///
/// Draws the section where the current article will be displayed.
class _ArticlesDisplay extends StatelessWidget {
  final Widget? article;

  const _ArticlesDisplay(this.article);

  @override
  Widget build(BuildContext context) {
    return Padding(
      padding: const EdgeInsets.only(
        top: 8,
      ),
      child: DecoratedBox(
        decoration: const BoxDecoration(
          border: Border.fromBorderSide(
            BorderSide(
              color: Colors.white24,
              width: 2,
            ),
          ),
        ),
        child: SizedBox(
          width: double.maxFinite,
          child: Stack(
            children: <Widget>[
              // --> Watermark display
              const Center(
                child: Opacity(
                  opacity: .2,
                  child: Image(
                    image: AssetImage(
                      TWSAAssets.fullLogoWhitePng,
                    ),
                    width: 325,
                  ),
                ),
              ),
              // --> Article display
              if (article != null) article!,
            ],
          ),
        ),
      ),
    );
  }
}

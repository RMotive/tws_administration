part of '../login_view.dart';

class _ConnectionHealthCard extends StatelessWidget {
  const _ConnectionHealthCard();

  @override
  Widget build(BuildContext context) {
    return ThemedWidget(
      builder: (ctx, theme) {
        return Center(
          child: DecoratedBox(
            decoration: BoxDecoration(
              color: theme.colorScheme.onBackground.withOpacity(.2),
              borderRadius: BorderRadius.circular(
                12,
              ),
            ),
            child: FittedBox(
              child: SizedBox(
                child: Padding(
                  padding: const EdgeInsets.symmetric(
                    vertical: 30,
                    horizontal: 40,
                  ),
                  child: Column(
                    mainAxisAlignment: MainAxisAlignment.spaceAround,
                    children: [
                      CircularProgressIndicator(
                        strokeWidth: 3,
                        strokeCap: StrokeCap.round,
                        color: theme.colorScheme.tertiary,
                      ),
                      const SizedBox(
                        height: 20,
                      ),
                      Text(
                        'Connecting and validating server health',
                        style: theme.textTheme.titleMedium,
                      ),
                    ],
                  ),
                ),
              ),
            ),
          ),
        );
      },
    );
  }
}

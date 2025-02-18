import 'dart:async';
import 'dart:convert';
import 'package:csm_client/csm_client.dart' hide JObject;
import 'package:csm_view/csm_view.dart';
import 'package:flutter/material.dart';
import 'package:tws_administration_view/core/router/twsa_routes.dart';
import 'package:tws_administration_view/data/services/sources.dart';
import 'package:tws_administration_view/data/storages/session_storage.dart';
import 'package:tws_administration_view/view/frames/article/action_ribbon_options.dart';
import 'package:tws_administration_view/view/frames/article/actions/maintenance_group_options.dart';
import 'package:tws_administration_view/view/pages/security/security_frame.dart';
import 'package:tws_administration_view/view/widgets/tws_article_table/tws_article_table.dart';
import 'package:tws_administration_view/view/widgets/tws_article_table/tws_article_table_adapter.dart';
import 'package:tws_administration_view/view/widgets/tws_article_table/tws_article_table_agent.dart';
import 'package:tws_administration_view/view/widgets/tws_article_table/tws_article_table_field_options.dart';
import 'package:tws_administration_view/view/widgets/tws_autocomplete_field/tws_autocomplete_adapter.dart';
import 'package:tws_administration_view/view/widgets/tws_confirmation_dialog.dart';
import 'package:tws_administration_view/view/widgets/tws_input_text.dart';
import 'package:tws_administration_view/view/widgets/tws_list_viewer/tws_list_viewer.dart';
import 'package:tws_administration_view/view/widgets/tws_option_selector.dart';
import 'package:tws_administration_view/view/widgets/tws_property_viewer.dart';
import 'package:tws_administration_view/view/widgets/tws_section.dart';
import 'package:tws_administration_view/view/widgets/tws_selectable_list.dart';
import 'package:tws_foundation_client/tws_foundation_client.dart';

part 'adapters/accounts_article_table_adapter.dart';
part 'whispers/accounts_article_state.dart';

final class AccountsArticle extends CSMPageBase {
  static final TWSArticleTableAgent agent = TWSArticleTableAgent();
  static final _AccountArticleState _pageState = _AccountArticleState();

  const AccountsArticle({super.key});

  @override
  Widget compose(BuildContext ctx, Size window) {
    return SecurityFrame(
      currentRoute: TWSARoutes.accountsArticle,
      actionsOptions: ActionRibbonOptions(
        refresher: agent.refresh,
        maintenanceGroupConfig: MaintenanceGroupOptions(
          onCreate: () => CSMRouter.i.drive(TWSARoutes.accountsCreateWhisper),
        ),
      ),
      article: CSMDynamicWidget<_AccountArticleState>(
        state: _pageState, 
        designer:(BuildContext ctx, _AccountArticleState state) {
          double optionsWidth = 250;

          return Column(
            children: <Widget>[
              SizedBox(
                width: double.maxFinite,
                child: Padding(
                  padding: const EdgeInsets.only(top: 16, bottom: 8.0, left: 8.0, right: 8.0),
                  child: Wrap(
                    alignment: WrapAlignment.center,
                    spacing: 12,
                    runSpacing: 12,
                    children: <Widget>[
                      TWSInputText(
                        label: 'Search by user',
                        deBounce: 600.miliseconds,
                        width: optionsWidth,
                        onChanged:(String text) => state.filterUser(text),
                      ),
                      TWSInputText(
                        label: 'Search by name',
                        deBounce: 600.miliseconds,
                        width: optionsWidth,
                        onChanged:(String text) => state.filterName(text),
                      ),
                      TWSInputText(
                        label: 'Search by father lastname',
                        deBounce: 600.miliseconds,
                        width: optionsWidth,
                        onChanged:(String text) => state.filterLastname(text),
                      ),
                      TWSInputText(
                        label: 'Search by mother email',
                        deBounce: 600.miliseconds,
                        width: optionsWidth,
                        onChanged:(String text) => state.filterEmail(text),
                      ),
                      TWSInputText(
                        label: 'Search by mother phone',
                        deBounce: 600.miliseconds,
                        width: optionsWidth,
                        onChanged:(String text) => state.filterPhone(text),
                      ),
                    ],
                  ),
                ),
              ),
              Expanded(
                child: TWSArticleTable<Account>(
                  adapter: _TableAdapter(_pageState),
                  agent: agent,
                  page: 1,
                  size: 25,
                  sizes: const <int>[25, 50, 75, 100],
                  onSelect: (bool isShowingDetails) {
                    // Method to prevent duplicated api calls when the details section is not visible.
                    // if(isShowingDetails) consumerAgent.refresh();
                    _isShowing = isShowingDetails;
                  },
                  fields: <TWSArticleTableFieldOptions<Account>>[
                    TWSArticleTableFieldOptions<Account>(
                      'User',
                      (Account item, int index, BuildContext ctx) => item.user,
                    ),
                    TWSArticleTableFieldOptions<Account>(
                      'Name',
                      (Account item, int index, BuildContext ctx) => item.contactNavigation?.name ?? "---",
                    ),
                    TWSArticleTableFieldOptions<Account>(
                      'Lastname',
                      (Account item, int index, BuildContext ctx) => item.contactNavigation?.lastName ?? "---",
                    ),
                    TWSArticleTableFieldOptions<Account>(
                      'Email',
                      (Account item, int index, BuildContext ctx) => item.contactNavigation?.email ?? "---",
                    ),
                    TWSArticleTableFieldOptions<Account>(
                      'Phone',
                      (Account item, int index, BuildContext ctx) => item.contactNavigation?.phone ?? "---",
                    ),
                  ],
                ),
              ),
            ],
          );
        },
      ),
    );
  }
}

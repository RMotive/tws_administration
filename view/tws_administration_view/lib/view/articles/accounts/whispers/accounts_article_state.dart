part of '../accounts_article.dart';
final class _AccountArticleState extends ReactorB {

  /// view filters initialization.
  List<ViewFilterNodeI<void>> accountsFilters = <ViewFilterNodeI<void>>[];
  // String userFilter = '';
  // String nameFilter = '';
  // String lastnameFilter = '';
  // String emailFilter = '';
  // String phoneFilter = '';

  // filterUser(String search) {
  //   userFilter = search;

  //   _composeFilters();
  // }
  // filterName(String search) {
  //   nameFilter = search;

  //   _composeFilters();
  // }

  // filterLastname(String search) {
  //   lastnameFilter = search;

  //   _composeFilters();
  // }

  // filterEmail(String search) {
  //   emailFilter = search;

  //   _composeFilters();
  // }

  // filterPhone(String search) {
  //   phoneFilter = search;

  //   _composeFilters();
  // }

  // _composeFilters() {
  //   accountsFilters = <ViewFilterNodeI<void>>[];
    
  //   if (userFilter.isNotEmpty) {
  //     ViewFilterNodeI<void> nFilter = ViewFilterProperty<>(0, SetViewFilterEvaluations.contians,Account.kUser, userFilter);
  //     accountsFilters.add(nFilter);
  //   }

  //   if (nameFilter.isNotEmpty) {
  //     SetViewFilterNodeInterface<Account> nFilter = SetViewPropertyFilter<Account>(0, SetViewFilterEvaluations.contians, 'ContactNavigation.name', nameFilter);
  //     accountsFilters.add(nFilter);
  //   }

  //   if (lastnameFilter.isNotEmpty) {
  //     SetViewFilterNodeInterface<Account> nFilter = SetViewPropertyFilter<Account>(0, SetViewFilterEvaluations.contians, 'ContactNavigation.lastname', lastnameFilter);
  //     accountsFilters.add(nFilter);
  //   }

  //   if (emailFilter.isNotEmpty) {
  //     SetViewFilterNodeInterface<Account> nFilter = SetViewPropertyFilter<Account>(0, SetViewFilterEvaluations.contians, 'ContactNavigation.email', emailFilter);
  //     accountsFilters.add(nFilter);
  //   }
    
  //   if (phoneFilter.isNotEmpty) {
  //     SetViewFilterNodeInterface<Account> nFilter = SetViewPropertyFilter<Account>(0, SetViewFilterEvaluations.contians, 'ContactNavigation.phone', phoneFilter);
  //     accountsFilters.add(nFilter);
  //   }

  //   effect();
  //   AccountsArticle.agent.refresh();
}
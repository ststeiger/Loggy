﻿// Generated by IcedCoffeeScript 108.0.11
(function() {
  ContentTools.StylePalette = (function() {
    function StylePalette() {}

    StylePalette._styles = [];

    StylePalette.add = function(styles) {
      return this._styles = this._styles.concat(styles);
    };

    StylePalette.styles = function(element) {
      var tagName;
      if (element === void 0) {
        return this._styles.slice();
      }
      tagName = element.tagName();
      return this._styles.filter(function(style) {
        if (!style._applicableTo) {
          return true;
        }
        return style._applicableTo.indexOf(tagName) !== -1;
      });
    };

    return StylePalette;

  })();

  ContentTools.Style = (function() {
    function Style(name, cssClass, applicableTo) {
      this._name = name;
      this._cssClass = cssClass;
      if (applicableTo) {
        this._applicableTo = applicableTo;
      } else {
        this._applicableTo = null;
      }
    }

    Style.prototype.applicableTo = function() {
      return this._applicableTo;
    };

    Style.prototype.cssClass = function() {
      return this._cssClass;
    };

    Style.prototype.name = function() {
      return this._name;
    };

    return Style;

  })();

}).call(this);
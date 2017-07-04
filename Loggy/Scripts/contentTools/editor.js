﻿// Generated by IcedCoffeeScript 108.0.11
(function() {
  var _EditorApp,
    __hasProp = {}.hasOwnProperty,
    __extends = function(child, parent) { for (var key in parent) { if (__hasProp.call(parent, key)) child[key] = parent[key]; } function ctor() { this.constructor = child; } ctor.prototype = parent.prototype; child.prototype = new ctor(); child.__super__ = parent.prototype; return child; },
    __slice = [].slice;

  _EditorApp = (function(_super) {
    __extends(_EditorApp, _super);

    function _EditorApp() {
      _EditorApp.__super__.constructor.call(this);
      this.history = null;
      this._state = 'dormant';
      this._busy = false;
      this._namingProp = null;
      this._fixtureTest = function(domElement) {
        return domElement.hasAttribute('data-fixture');
      };
      this._regionQuery = null;
      this._domRegions = null;
      this._regions = {};
      this._orderedRegions = [];
      this._rootLastModified = null;
      this._regionsLastModified = {};
      this._ignition = null;
      this._inspector = null;
      this._toolbox = null;
      this._emptyRegionsAllowed = false;
    }

    _EditorApp.prototype.ctrlDown = function() {
      return this._ctrlDown;
    };

    _EditorApp.prototype.domRegions = function() {
      return this._domRegions;
    };

    _EditorApp.prototype.getState = function() {
      return this._state;
    };

    _EditorApp.prototype.ignition = function() {
      return this._ignition;
    };

    _EditorApp.prototype.inspector = function() {
      return this._inspector;
    };

    _EditorApp.prototype.isDormant = function() {
      return this._state === 'dormant';
    };

    _EditorApp.prototype.isReady = function() {
      return this._state === 'ready';
    };

    _EditorApp.prototype.isEditing = function() {
      return this._state === 'editing';
    };

    _EditorApp.prototype.orderedRegions = function() {
      var name;
      return (function() {
        var _i, _len, _ref, _results;
        _ref = this._orderedRegions;
        _results = [];
        for (_i = 0, _len = _ref.length; _i < _len; _i++) {
          name = _ref[_i];
          _results.push(this._regions[name]);
        }
        return _results;
      }).call(this);
    };

    _EditorApp.prototype.regions = function() {
      return this._regions;
    };

    _EditorApp.prototype.shiftDown = function() {
      return this._shiftDown;
    };

    _EditorApp.prototype.toolbox = function() {
      return this._toolbox;
    };

    _EditorApp.prototype.busy = function(busy) {
      if (busy === void 0) {
        this._busy = busy;
      }
      this._busy = busy;
      if (this._ignition) {
        return this._ignition.busy(busy);
      }
    };

    _EditorApp.prototype.createPlaceholderElement = function(region) {
      return new ContentEdit.Text('p', {}, '');
    };

    _EditorApp.prototype.init = function(queryOrDOMElements, namingProp, fixtureTest, withIgnition) {
      if (namingProp == null) {
        namingProp = 'id';
      }
      if (fixtureTest == null) {
        fixtureTest = null;
      }
      if (withIgnition == null) {
        withIgnition = true;
      }
      this._namingProp = namingProp;
      if (fixtureTest) {
        this._fixtureTest = fixtureTest;
      }
      this.mount();
      if (withIgnition) {
        this._ignition = new ContentTools.IgnitionUI();
        this.attach(this._ignition);
        this._ignition.addEventListener('edit', (function(_this) {
          return function(ev) {
            ev.preventDefault();
            _this.start();
            return _this._ignition.state('editing');
          };
        })(this));
        this._ignition.addEventListener('confirm', (function(_this) {
          return function(ev) {
            ev.preventDefault();
            if (_this._ignition.state() !== 'editing') {
              return;
            }
            _this._ignition.state('ready');
            return _this.stop(true);
          };
        })(this));
        this._ignition.addEventListener('cancel', (function(_this) {
          return function(ev) {
            ev.preventDefault();
            if (_this._ignition.state() !== 'editing') {
              return;
            }
            _this.stop(false);
            if (_this.isEditing()) {
              return _this._ignition.state('editing');
            } else {
              return _this._ignition.state('ready');
            }
          };
        })(this));
      }
      this._toolbox = new ContentTools.ToolboxUI(ContentTools.DEFAULT_TOOLS);
      this.attach(this._toolbox);
      this._inspector = new ContentTools.InspectorUI();
      this.attach(this._inspector);
      this._state = 'ready';
      this._handleDetach = (function(_this) {
        return function(element) {
          return _this._preventEmptyRegions();
        };
      })(this);
      this._handleClipboardPaste = (function(_this) {
        return function(element, ev) {
          var clipboardData;
          clipboardData = null;
          if (ev.clipboardData) {
            clipboardData = ev.clipboardData.getData('text/plain');
          }
          if (window.clipboardData) {
            clipboardData = window.clipboardData.getData('TEXT');
          }
          return _this.paste(element, clipboardData);
        };
      })(this);
      this._handleNextRegionTransition = (function(_this) {
        return function(region) {
          var child, element, index, regions, _i, _len, _ref;
          regions = _this.orderedRegions();
          index = regions.indexOf(region);
          if (index >= (regions.length - 1)) {
            return;
          }
          region = regions[index + 1];
          element = null;
          _ref = region.descendants();
          for (_i = 0, _len = _ref.length; _i < _len; _i++) {
            child = _ref[_i];
            if (child.content !== void 0) {
              element = child;
              break;
            }
          }
          if (element) {
            element.focus();
            element.selection(new ContentSelect.Range(0, 0));
            return;
          }
          return ContentEdit.Root.get().trigger('next-region', region);
        };
      })(this);
      this._handlePreviousRegionTransition = (function(_this) {
        return function(region) {
          var child, descendants, element, index, length, regions, _i, _len;
          regions = _this.orderedRegions();
          index = regions.indexOf(region);
          if (index <= 0) {
            return;
          }
          region = regions[index - 1];
          element = null;
          descendants = region.descendants();
          descendants.reverse();
          for (_i = 0, _len = descendants.length; _i < _len; _i++) {
            child = descendants[_i];
            if (child.content !== void 0) {
              element = child;
              break;
            }
          }
          if (element) {
            length = element.content.length();
            element.focus();
            element.selection(new ContentSelect.Range(length, length));
            return;
          }
          return ContentEdit.Root.get().trigger('previous-region', region);
        };
      })(this);
      ContentEdit.Root.get().bind('detach', this._handleDetach);
      ContentEdit.Root.get().bind('paste', this._handleClipboardPaste);
      ContentEdit.Root.get().bind('next-region', this._handleNextRegionTransition);
      ContentEdit.Root.get().bind('previous-region', this._handlePreviousRegionTransition);
      return this.syncRegions(queryOrDOMElements);
    };

    _EditorApp.prototype.destroy = function() {
      ContentEdit.Root.get().unbind('detach', this._handleDetach);
      ContentEdit.Root.get().unbind('paste', this._handleClipboardPaste);
      ContentEdit.Root.get().unbind('next-region', this._handleNextRegionTransition);
      ContentEdit.Root.get().unbind('previous-region', this._handlePreviousRegionTransition);
      return this.unmount();
    };

    _EditorApp.prototype.highlightRegions = function(highlight) {
      var domRegion, _i, _len, _ref, _results;
      _ref = this._domRegions;
      _results = [];
      for (_i = 0, _len = _ref.length; _i < _len; _i++) {
        domRegion = _ref[_i];
        if (highlight) {
          _results.push(ContentEdit.addCSSClass(domRegion, 'ct--highlight'));
        } else {
          _results.push(ContentEdit.removeCSSClass(domRegion, 'ct--highlight'));
        }
      }
      return _results;
    };

    _EditorApp.prototype.mount = function() {
      this._domElement = this.constructor.createDiv(['ct-app']);
      document.body.insertBefore(this._domElement, null);
      return this._addDOMEventListeners();
    };

    _EditorApp.prototype.paste = function(element, clipboardData) {
      var character, content, cursor, encodeHTML, i, insertAt, insertIn, insertNode, item, itemText, lastItem, line, lineLength, lines, replaced, selection, spawn, tags, tail, tip, type, _i, _len;
      content = clipboardData;
      lines = content.split('\n');
      lines = lines.filter(function(line) {
        return line.trim() !== '';
      });
      if (!lines) {
        return;
      }
      encodeHTML = HTMLString.String.encode;
      spawn = true;
      type = element.type();
      if (lines.length === 1) {
        spawn = false;
      }
      if (type === 'PreText') {
        spawn = false;
      }
      if (!element.can('spawn')) {
        spawn = false;
      }
      if (spawn) {
        if (type === 'ListItemText') {
          insertNode = element.parent();
          insertIn = element.parent().parent();
          insertAt = insertIn.children.indexOf(insertNode) + 1;
        } else {
          insertNode = element;
          if (insertNode.parent().type() !== 'Region') {
            insertNode = element.closest(function(node) {
              return node.parent().type() === 'Region';
            });
          }
          insertIn = insertNode.parent();
          insertAt = insertIn.children.indexOf(insertNode) + 1;
        }
        for (i = _i = 0, _len = lines.length; _i < _len; i = ++_i) {
          line = lines[i];
          line = encodeHTML(line);
          if (type === 'ListItemText') {
            item = new ContentEdit.ListItem();
            itemText = new ContentEdit.ListItemText(line);
            item.attach(itemText);
            lastItem = itemText;
          } else {
            item = new ContentEdit.Text('p', {}, line);
            lastItem = item;
          }
          insertIn.attach(item, insertAt + i);
        }
        lineLength = lastItem.content.length();
        lastItem.focus();
        return lastItem.selection(new ContentSelect.Range(lineLength, lineLength));
      } else {
        content = encodeHTML(content);
        content = new HTMLString.String(content, type === 'PreText');
        selection = element.selection();
        cursor = selection.get()[0] + content.length();
        tip = element.content.substring(0, selection.get()[0]);
        tail = element.content.substring(selection.get()[1]);
        replaced = element.content.substring(selection.get()[0], selection.get()[1]);
        if (replaced.length()) {
          character = replaced.characters[0];
          tags = character.tags();
          if (character.isTag()) {
            tags.shift();
          }
          if (tags.length >= 1) {
            content = content.format.apply(content, [0, content.length()].concat(__slice.call(tags)));
          }
        }
        element.content = tip.concat(content);
        element.content = element.content.concat(tail, false);
        element.updateInnerHTML();
        element.taint();
        selection.set(cursor, cursor);
        return element.selection(selection);
      }
    };

    _EditorApp.prototype.unmount = function() {
      if (!this.isMounted()) {
        return;
      }
      this._domElement.parentNode.removeChild(this._domElement);
      this._domElement = null;
      this._removeDOMEventListeners();
      this._ignition = null;
      this._inspector = null;
      return this._toolbox = null;
    };

    _EditorApp.prototype.revert = function() {
      var confirmMessage;
      if (!this.dispatchEvent(this.createEvent('revert'))) {
        return;
      }
      confirmMessage = ContentEdit._('Your changes have not been saved, do you really want to lose them?');
      if (ContentEdit.Root.get().lastModified() > this._rootLastModified && !window.confirm(confirmMessage)) {
        return false;
      }
      this.revertToSnapshot(this.history.goTo(0), false);
      return true;
    };

    _EditorApp.prototype.revertToSnapshot = function(snapshot, restoreEditable) {
      var child, name, region, _i, _len, _ref, _ref1, _ref2;
      if (restoreEditable == null) {
        restoreEditable = true;
      }
      _ref = this._regions;
      for (name in _ref) {
        region = _ref[name];
        _ref1 = region.children;
        for (_i = 0, _len = _ref1.length; _i < _len; _i++) {
          child = _ref1[_i];
          child.unmount();
        }
        region.domElement().innerHTML = snapshot.regions[name];
      }
      if (restoreEditable) {
        if (ContentEdit.Root.get().focused()) {
          ContentEdit.Root.get().focused().blur();
        }
        this._regions = {};
        this.syncRegions(null, true);
        ContentEdit.Root.get()._modified = snapshot.rootModified;
        _ref2 = this._regions;
        for (name in _ref2) {
          region = _ref2[name];
          if (snapshot.regionModifieds[name]) {
            region._modified = snapshot.regionModifieds[name];
          }
        }
        this.history.replaceRegions(this._regions);
        this.history.restoreSelection(snapshot);
        return this._inspector.updateTags();
      }
    };

    _EditorApp.prototype.save = function(passive) {
      var child, html, modifiedRegions, name, region, root, _i, _len, _ref, _ref1;
      if (!this.dispatchEvent(this.createEvent('save', {
        passive: passive
      }))) {
        return;
      }
      root = ContentEdit.Root.get();
      if (root.lastModified() === this._rootLastModified && passive) {
        this.dispatchEvent(this.createEvent('saved', {
          regions: {},
          passive: passive
        }));
        return;
      }
      modifiedRegions = {};
      _ref = this._regions;
      for (name in _ref) {
        region = _ref[name];
        html = region.html();
        if (region.children.length === 1) {
          child = region.children[0];
          if (child.content && !child.content.html()) {
            html = '';
          }
        }
        if (!passive) {
          _ref1 = region.children;
          for (_i = 0, _len = _ref1.length; _i < _len; _i++) {
            child = _ref1[_i];
            child.unmount();
          }
          region.domElement().innerHTML = html;
        }
        if (region.lastModified() === this._regionsLastModified[name]) {
          continue;
        }
        modifiedRegions[name] = html;
        this._regionsLastModified[name] = region.lastModified();
      }
      return this.dispatchEvent(this.createEvent('saved', {
        regions: modifiedRegions,
        passive: passive
      }));
    };

    _EditorApp.prototype.setRegionOrder = function(regionNames) {
      return this._orderedRegions = regionNames.slice();
    };

    _EditorApp.prototype.start = function() {
      if (!this.dispatchEvent(this.createEvent('start'))) {
        return;
      }
      this.busy(true);
      this.syncRegions();
      this._initRegions();
      this._preventEmptyRegions();
      this._rootLastModified = ContentEdit.Root.get().lastModified();
      this.history = new ContentTools.History(this._regions);
      this.history.watch();
      this._state = 'editing';
      this._toolbox.show();
      this._inspector.show();
      return this.busy(false);
    };

    _EditorApp.prototype.stop = function(save) {
      var focused;
      if (!this.dispatchEvent(this.createEvent('stop', {
        save: save
      }))) {
        return;
      }
      focused = ContentEdit.Root.get().focused();
      if (focused && focused.isMounted() && focused._syncContent !== void 0) {
        focused._syncContent();
      }
      if (save) {
        this.save();
      } else {
        if (!this.revert()) {
          return;
        }
      }
      this.history.stopWatching();
      this.history = null;
      this._toolbox.hide();
      this._inspector.hide();
      this._regions = {};
      this._state = 'ready';
      if (ContentEdit.Root.get().focused()) {
        this._allowEmptyRegions((function(_this) {
          return function() {
            return ContentEdit.Root.get().focused().blur();
          };
        })(this));
      }
    };

    _EditorApp.prototype.syncRegions = function(regionQuery, restoring) {
      if (regionQuery) {
        this._regionQuery = regionQuery;
      }
      this._domRegions = [];
      if (this._regionQuery) {
        if (typeof this._regionQuery === 'string' || this._regionQuery instanceof String) {
          this._domRegions = document.querySelectorAll(this._regionQuery);
        } else {
          this._domRegions = this._regionQuery;
        }
      }
      if (this._state === 'editing') {
        this._initRegions(restoring);
        this._preventEmptyRegions();
      }
      if (this._ignition) {
        if (this._domRegions.length) {
          return this._ignition.show();
        } else {
          return this._ignition.hide();
        }
      }
    };

    _EditorApp.prototype._addDOMEventListeners = function() {
      this._handleHighlightOn = (function(_this) {
        return function(ev) {
          var _ref;
          if ((_ref = ev.keyCode) === 17 || _ref === 224 || _ref === 91 || _ref === 93) {
            _this._ctrlDown = true;
          }
          if (ev.keyCode === 16 && !_this._ctrlDown) {
            if (_this._highlightTimeout) {
              return;
            }
            _this._shiftDown = true;
            _this._highlightTimeout = setTimeout(function() {
              return _this.highlightRegions(true);
            }, ContentTools.HIGHLIGHT_HOLD_DURATION);
            return;
          }
          clearTimeout(_this._highlightTimeout);
          return _this.highlightRegions(false);
        };
      })(this);
      this._handleHighlightOff = (function(_this) {
        return function(ev) {
          var _ref;
          if ((_ref = ev.keyCode) === 17 || _ref === 224) {
            _this._ctrlDown = false;
            return;
          }
          if (ev.keyCode === 16) {
            _this._shiftDown = false;
            if (_this._highlightTimeout) {
              clearTimeout(_this._highlightTimeout);
              _this._highlightTimeout = null;
            }
            return _this.highlightRegions(false);
          }
        };
      })(this);
      this._handleVisibility = (function(_this) {
        return function(ev) {
          if (!document.hasFocus()) {
            clearTimeout(_this._highlightTimeout);
            return _this.highlightRegions(false);
          }
        };
      })(this);
      document.addEventListener('keydown', this._handleHighlightOn);
      document.addEventListener('keyup', this._handleHighlightOff);
      document.addEventListener('visibilitychange', this._handleVisibility);
      this._handleBeforeUnload = (function(_this) {
        return function(ev) {
          var cancelMessage;
          if (_this._state === 'editing') {
            cancelMessage = ContentEdit._(ContentTools.CANCEL_MESSAGE);
            (ev || window.event).returnValue = cancelMessage;
            return cancelMessage;
          }
        };
      })(this);
      window.addEventListener('beforeunload', this._handleBeforeUnload);
      this._handleUnload = (function(_this) {
        return function(ev) {
          return _this.destroy();
        };
      })(this);
      return window.addEventListener('unload', this._handleUnload);
    };

    _EditorApp.prototype._allowEmptyRegions = function(callback) {
      this._emptyRegionsAllowed = true;
      callback();
      return this._emptyRegionsAllowed = false;
    };

    _EditorApp.prototype._preventEmptyRegions = function() {
      var child, hasEditableChildren, lastModified, name, placeholder, region, _i, _len, _ref, _ref1, _results;
      if (this._emptyRegionsAllowed) {
        return;
      }
      _ref = this._regions;
      _results = [];
      for (name in _ref) {
        region = _ref[name];
        lastModified = region.lastModified();
        hasEditableChildren = false;
        _ref1 = region.children;
        for (_i = 0, _len = _ref1.length; _i < _len; _i++) {
          child = _ref1[_i];
          if (child.type() !== 'Static') {
            hasEditableChildren = true;
            break;
          }
        }
        if (hasEditableChildren) {
          continue;
        }
        placeholder = this.createPlaceholderElement(region);
        region.attach(placeholder);
        _results.push(region._modified = lastModified);
      }
      return _results;
    };

    _EditorApp.prototype._removeDOMEventListeners = function() {
      document.removeEventListener('keydown', this._handleHighlightOn);
      document.removeEventListener('keyup', this._handleHighlightOff);
      window.removeEventListener('beforeunload', this._handleBeforeUnload);
      return window.removeEventListener('unload', this._handleUnload);
    };

    _EditorApp.prototype._initRegions = function(restoring) {
      var domRegion, found, i, index, name, region, _i, _len, _ref, _ref1, _results;
      if (restoring == null) {
        restoring = false;
      }
      found = {};
      this._orderedRegions = [];
      _ref = this._domRegions;
      for (i = _i = 0, _len = _ref.length; _i < _len; i = ++_i) {
        domRegion = _ref[i];
        name = domRegion.getAttribute(this._namingProp);
        if (!name) {
          name = i;
        }
        found[name] = true;
        this._orderedRegions.push(name);
        if (this._regions[name] && this._regions[name].domElement() === domRegion) {
          continue;
        }
        if (this._fixtureTest(domRegion)) {
          this._regions[name] = new ContentEdit.Fixture(domRegion);
        } else {
          this._regions[name] = new ContentEdit.Region(domRegion);
        }
        if (!restoring) {
          this._regionsLastModified[name] = this._regions[name].lastModified();
        }
      }
      _ref1 = this._regions;
      _results = [];
      for (name in _ref1) {
        region = _ref1[name];
        if (found[name]) {
          continue;
        }
        delete this._regions[name];
        delete this._regionsLastModified[name];
        index = this._orderedRegions.indexOf(name);
        if (index > -1) {
          _results.push(this._orderedRegions.splice(index, 1));
        } else {
          _results.push(void 0);
        }
      }
      return _results;
    };

    return _EditorApp;

  })(ContentTools.ComponentUI);

  ContentTools.EditorApp = (function() {
    var instance;

    function EditorApp() {}

    instance = null;

    EditorApp.get = function() {
      var cls;
      cls = ContentTools.EditorApp.getCls();
      return instance != null ? instance : instance = new cls();
    };

    EditorApp.getCls = function() {
      return _EditorApp;
    };

    return EditorApp;

  })();

}).call(this);
